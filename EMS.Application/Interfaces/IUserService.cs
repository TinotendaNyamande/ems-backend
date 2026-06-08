using EMS.Application.Dtos.Auth;

namespace EMS.Application.Interfaces
{
    public interface IUserService
    {
        Task AddUserToOrganisationAsync(Guid organisationId, string userId);
        Task<UserDto> GetUserByIdAsync (string userId);
        Task<IEnumerable<UserDto>> GetUsersInOrganisationAsync(Guid organisationId);
        Task<Guid?> CheckIfUserBelongsToAnyOrganisationAsync(string userId);
        Task<Guid?> CheckIfUserIsOrganisationOwner(string userId);

    }
}
