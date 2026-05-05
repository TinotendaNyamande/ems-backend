using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using EMS.Infrastructure.persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class UserService(
            UserManager<ApplicationUser> userManager,
            ILogger<AuthService> logger,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context
            ) : IUserService
    {
        public async Task AssignRoleAsync(string roleName, string userId)
        {
            logger.LogInformation("Attemping to assign role :{roleName} to user : {userId}", roleName, userId);
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                logger.LogError("Role name {roleName} not found", roleName);
                throw new ResourceNotFoundException($"Role {roleName}", null);
            }
            var user = await userManager.FindByIdAsync(userId) ?? throw new ResourceNotFoundException($"User", userId);
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                await userManager.AddToRoleAsync(user, roleName);
                logger.LogInformation("Successfully assigned role :{roleName} to user : {userId}", roleName, userId);
            }

        }

        public async Task AddUserToCompanyAsync(Guid organisationId, string userId)
        {
            logger.LogInformation("Attempting to add user : {userId} to organisation {companyId}", userId, organisationId);
            var user = await userManager.FindByIdAsync(userId) ?? throw new ResourceNotFoundException($"User", userId);
            var organisation = await context.Organisations.FindAsync(organisationId)
                ?? throw new ResourceNotFoundException($"Organisation", organisationId);
            user.AddOrganisationIdToUser(organisationId);
            await context.SaveChangesAsync();
            logger.LogInformation("Successfully added user : {userId} to organisation {companyId}", userId, organisationId);
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return new UserDto
            {
                Id = user.Id,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email
            };
        }
    }
}
