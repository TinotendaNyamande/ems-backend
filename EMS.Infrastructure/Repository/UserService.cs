using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using EMS.Infrastructure.persistence;
using Microsoft.AspNetCore.Identity;
using EMS.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class UserService(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context
            ) : IUserService
    {


        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId)
                ?? throw new ResourceNotFoundException($"User", userId);
            var roles = await userManager.GetRolesAsync(user);
            return new UserDto(user.Id, user.FirstName, user.LastName, user.Email, roles.FirstOrDefault() ?? string.Empty);
 
        }

        public Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var query = from user in context.Users
                        join userRole in context.UserRoles on user.Id equals userRole.UserId
                        join role in context.Roles on userRole.RoleId equals role.Id
                        select new UserDto(user.Id, user.FirstName, user.LastName, user.Email, role.Name);
    

            return Task.FromResult(query.AsEnumerable());
        }
    }
}
