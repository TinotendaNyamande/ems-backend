using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IOrganisationUserRoleRepository
    {
        Task<IEnumerable<OrganisationUserRole>> GetRolesByUserIdAsync(string userId);
         Task AddRoleToUserAsync(OrganisationUserRole organisationUserRole);
        Task RemoveRolesFromUserAsync(string UserId);
    }
}
