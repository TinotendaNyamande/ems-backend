using EMS.Application.Dtos.Auth;

namespace EMS.Application.Interfaces
{
    public interface IUserService
    {
        Task AssignRoleAsync(string roleName, string userId);
        Task AddUserToCompanyAsync(Guid organisationId, string userId);
        Task<UserDto> GetUserByIdAsync (string userId);
    }
}
