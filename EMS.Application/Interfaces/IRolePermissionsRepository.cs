using EMS.Application.Dtos.RolesAndPermissions;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IRolePermissionsRepository
    {
        Task EditPermissionsAsync(Guid permissionId,EditPermissionDto editPermissionDto);
        Task CreatePermissionAsync(OrganisationRolePermission permission);
        Task<OrganisationRolePermission> GetPermissionAsync(Guid permissionId);
        Task<IEnumerable<OrganisationRolePermission>> GetPermissionsForRole(Guid roleId);
    }
}
