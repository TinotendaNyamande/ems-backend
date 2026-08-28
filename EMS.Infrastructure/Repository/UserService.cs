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
            ApplicationDbContext context
            ) : IUserService
    {


        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId)
                ?? throw new ResourceNotFoundException($"User", userId);
            var roles = await userManager.GetRolesAsync(user);
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };
        }

        public Task<IEnumerable<UserDto>> GetUsersInOrganisationAsync()
        {
            var query = from user in context.Users
                        join userRole in context.UserRoles on user.Id equals userRole.UserId
                        join role in context.Roles on userRole.RoleId equals role.Id
                        select new UserDto
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Role = role.Name
                        };

            return Task.FromResult(query.AsEnumerable());
        }
    }
}
