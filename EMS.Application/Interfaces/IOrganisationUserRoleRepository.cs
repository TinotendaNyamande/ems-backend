using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IOrganisationUserRoleRepository
    {
         Task AddRoleToUserAsync(OrganisationUserRole organisationUserRole);
        Task RemoveRolesFromUserAsync(string UserId);
        Task<OrganisationUserRole> GetRoleByUserIdAsync(string userId);


    }
}
