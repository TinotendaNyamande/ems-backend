using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IRolesRepository
    {
        Task<IEnumerable<OrganisationRole>> GetRolesForOrganisationAsync(Guid organisationId);
        Task<OrganisationRole> GetRoleByIdAsync (Guid roleId);
        Task CreateRoleAsync(OrganisationRole organisationRole);
        Task CreateDefaultRolesAndPermissionsAsync(Guid organisationId);
        Task<bool> CanAccess(string permission, string userId);
        Task<OrganisationRole> GetRoleByNameAsync(string roleName, Guid organisationId);
    }
}
