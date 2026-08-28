using EMS.Application.Dtos.Auth;

namespace EMS.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync (string userId);
        Task<IEnumerable<UserDto>> GetUsersInOrganisationAsync();
  
    }
}
