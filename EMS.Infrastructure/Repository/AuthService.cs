using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using EMS.Infrastructure.persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;
using EMS.Domain.Models;
using EMS.Application.Dtos.RolesAndPermissions;

namespace EMS.Infrastructure.Repository
{
    internal class AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ILogger<AuthService> logger,
            IOrganisationUserRoleRepository organisationUserRoleRepository,
            IRolePermissionsRepository rolePermissionsRepository
            ) : IAuthService
    {



        public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto request)
        {
            logger.LogInformation("Register attempt for {Email}", request.Email);

            var existingUser = await userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                logger.LogWarning("Register failed because user already exists: {Email}", existingUser.Email);
                throw new InvalidOperationException("User already exists");
            }
            ApplicationUser user;

            user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,

            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                logger.LogError("Register failed for {Email}: {Errors}", request.Email, errors);
                throw new Exception(errors);
            }



            var accessToken = await GenerateTokenAsync(user);
            var refreshToken = GenerateRefreshToken();

            await SaveRefreshTokenAsync(user, refreshToken);
            logger.LogInformation("User registered successfully: {Email} ({UserId})", user.Email, user.Id);
            var permissions = await this.GetUserPermissionsAsync(user.Id);

            return new AuthResponseDto(accessToken, user.Email!, user.Id, refreshToken,"")
            {
                Permissions = permissions.Select(p => new GetPermissionDto
                {
                    Id = p.Id,
                    OrganisationRoleId = p.OrganisationRoleId,
                    PermissionKey = p.PermissionKey,
                    IsAllowed = p.IsAllowed,
                })
            };

        }

        public async Task<AuthResponseDto> LoginAsync(LoginUserDto request)
        {
            logger.LogInformation("Login attempt for {Email}", request.Email);

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                logger.LogWarning("Login failed because email was not found: {Email}", request.Email);
                throw new UnauthorizedAccessException("Invalid email or password");
            }
                
            var result = await signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                false
            );

            if (!result.Succeeded)
            {
                logger.LogWarning("Login failed because credentials were invalid: {Email} ({UserId})", user.Email, user.Id);
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            user.UpdateLastLoginDate();
            await userManager.UpdateAsync(user);

            var accessToken = await GenerateTokenAsync(user);
            var refreshToken = GenerateRefreshToken();

            await SaveRefreshTokenAsync(user, refreshToken);
            logger.LogInformation("User logged in successfully: {Email} ({UserId})", user.Email, user.Id);
            var permissions = await this.GetUserPermissionsAsync(user.Id);
            var role = await organisationUserRoleRepository.GetRoleByUserIdAsync(user.Id);
            return new AuthResponseDto(accessToken, user.Email!, user.Id, refreshToken, role?.Role.RoleName)
            {
                Permissions = permissions.Select(p => new GetPermissionDto
                {
                    Id = p.Id,
                    OrganisationRoleId = p.OrganisationRoleId,
                    PermissionKey = p.PermissionKey,
                    IsAllowed = p.IsAllowed,
                })
            };

        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            logger.LogInformation("Refresh token rotation attempt");

            var tokenHash = HashToken(refreshToken);
            var user = await userManager.Users
                 .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens != null && u.RefreshTokens.Any(t =>
                    t.TokenHash == tokenHash || t.Token == refreshToken));
            if (user == null)
            {
                logger.LogWarning("Refresh token rotation failed because token was not found");
                throw new Exception("Invalid refresh token");
            }

            var matchedToken = (user.RefreshTokens?
                .FirstOrDefault(t => t.TokenHash == tokenHash || t.Token == refreshToken));
            if(matchedToken == null)
            {
                logger.LogWarning("Refresh token rotation failed because matching token was not found for user {UserId}", user.Id);
                throw new Exception("Invalid refresh token");
            }
            if (matchedToken.IsRevoked || matchedToken.ExpiresAt <= DateTime.UtcNow)
            {
                foreach (var t in user.RefreshTokens ?? Enumerable.Empty<RefreshToken>())
                {
                    t.IsRevoked = true;
                }
                await userManager.UpdateAsync(user);
                logger.LogWarning("Refresh token rotation failed because token is revoked or expired for user {UserId}", user.Id);
                throw new UnauthorizedAccessException("Refresh token is no longer valid. Please log in again.");
            }

            CleanupRefreshTokens(user);
            var newAccessToken = await GenerateTokenAsync(user);
            var newRefreshToken = GenerateRefreshToken();

            var oldToken = (user.RefreshTokens?
                .FirstOrDefault(t => t.TokenHash == tokenHash || t.Token == refreshToken)) ?? throw new Exception("Invalid refresh token");
            oldToken.IsRevoked = true;
            if (oldToken.Token != null)
            {
                oldToken.TokenHash = HashToken(oldToken.Token);
                oldToken.Token = null;
            }

            await SaveRefreshTokenAsync(user, newRefreshToken);
            logger.LogInformation("Refresh token rotated successfully for user {UserId}", user.Id);
            var permissions = await this.GetUserPermissionsAsync(user.Id);
            var role = await organisationUserRoleRepository.GetRoleByUserIdAsync(user.Id);
            return new AuthResponseDto(newAccessToken, user.Email!, user.Id, newRefreshToken, role?.Role.RoleName)
            {
                Permissions = permissions.Select(p => new GetPermissionDto
                {
                    Id = p.Id,
                    OrganisationRoleId = p.OrganisationRoleId,
                    PermissionKey = p.PermissionKey,
                    IsAllowed = p.IsAllowed,
                })
            };
 
        }
        public async Task LogoutAsync(string refreshToken)
        {
            logger.LogInformation("Logout attempt");

            var tokenHash = HashToken(refreshToken);
            var user = await userManager.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens != null && u.RefreshTokens.Any(t => t.TokenHash == tokenHash || t.Token == refreshToken));

            if (user == null)
            {
                logger.LogWarning("Logout skipped because refresh token was not found");
                return;
            }

            var token = user.RefreshTokens?
                .FirstOrDefault(t => t.TokenHash == tokenHash || t.Token == refreshToken);
            if (token == null)
            {
                logger.LogWarning("Logout skipped because matching token was not found for user {UserId}", user.Id);
                return;
            }

            token.IsRevoked = true;
            if (token.Token != null)
            {
                token.TokenHash = HashToken(token.Token);
                token.Token = null;
            }

            await userManager.UpdateAsync(user);
            logger.LogInformation("User logged out successfully: {UserId}", user.Id);
        }


        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim("FirstName", user.FirstName),
                new Claim("OrganisationId", user.OrganisationId.HasValue ? user.OrganisationId.Value.ToString() : string.Empty)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim("Role", role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );
            var expirySetting = configuration["Jwt:ExpiryDays"];

            double expiryDays = 30;

            if (double.TryParse(expirySetting, out var parsed))
            {
                expiryDays = parsed;
            }

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expiryDays),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
        private async Task SaveRefreshTokenAsync(ApplicationUser user, string refreshToken)
        {
            var token = new RefreshToken
            {
                Token = null,
                TokenHash = HashToken(refreshToken),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                IsRevoked = false
            };

            user.RefreshTokens ??= new List<RefreshToken>();
            user.RefreshTokens.Add(token);

            await userManager.UpdateAsync(user);
            logger.LogInformation("Refresh token saved for user {UserId}; expires at {ExpiresAt:u}", user.Id, token.ExpiresAt);
        }

        private static string HashToken(string token)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(hash);
        }

        private static void CleanupRefreshTokens(ApplicationUser user)
        {
            if (user.RefreshTokens == null) return;

            var now = DateTime.UtcNow;
            var toRemove = user.RefreshTokens
                .Where(t => t.IsRevoked || t.ExpiresAt <= now)
                .ToList();

            foreach (var token in toRemove)
            {
                user.RefreshTokens.Remove(token);
            }
        }

        public async Task ChangePasswordAsync(string userId, ChangeUserPasswordDto changePasswordDto)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new ResourceNotFoundException("User not found", null);
            await userManager.ChangePasswordAsync(user, changePasswordDto.Password, changePasswordDto.NewPassword);
        }

        public async Task<string> CreateUserForOrganisationAsync(string role, RegisterUserDto request)
        {
            logger.LogInformation("Register attempt for user with email: {Email}", request.Email);

            var existingUser = await userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)   
            {
                logger.LogWarning("Register failed because user already exists: {Email}", existingUser.Email);
                throw new InvalidOperationException("User already exists");
            }
            ApplicationUser user;

            user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,

            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                logger.LogError("Register failed for {Email}: {Errors}", request.Email, errors);
                throw new Exception(errors);
            }

            //await userManager.AddToRoleAsync(user, role);
           // logger.LogInformation("Assigned role {Role} to user {UserId}", role, user.Id);

            var accessToken = await GenerateTokenAsync(user);
            var refreshToken = GenerateRefreshToken();

            await SaveRefreshTokenAsync(user, refreshToken);
            logger.LogInformation("User registered successfully: {Email} ({UserId})", user.Email, user.Id);
            return user.Id;

        }
        public async Task<IEnumerable<OrganisationRolePermission>> GetUserPermissionsAsync(string userId)
        {
            var userRole = await organisationUserRoleRepository.GetRoleByUserIdAsync(userId);
            if(userRole != null)
            {
                return await rolePermissionsRepository.GetPermissionsForRole(userRole.RoleId);
            }
            else
            {
                return [];
            }
        }


    }
}
