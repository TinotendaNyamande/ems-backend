using EMS.Application.Dtos.RolesAndPermissions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class RolePermissionsRepository(ApplicationDbContext context) : IRolePermissionsRepository
    {
        public async Task CreatePermissionAsync(OrganisationRolePermission permission)
        {
            context.Add(permission);
            await context.SaveChangesAsync();
        }

        public async Task EditPermissionsAsync(Guid permissionId, EditPermissionDto editPermissionDto)
        {
            var permission = await context.OrganisationRolePermissions
              .AsNoTracking()
              .Where(p => p.Id == permissionId)
              .FirstOrDefaultAsync()
              ?? throw new ResourceNotFoundException("Permission", permissionId);
            permission.ChangePermission(editPermissionDto.IsAllowed);
            await context.SaveChangesAsync();
        }

        public async Task<OrganisationRolePermission> GetPermissionAsync(Guid permissionId)
        {
            return await context.OrganisationRolePermissions
                .AsNoTracking()
                .Where(p=>p.Id==permissionId)
                .FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException("Permission",permissionId);
        }

        public async Task<IEnumerable<OrganisationRolePermission>> GetPermissionsForRole(Guid roleId)
        {
            return await context.OrganisationRolePermissions
                .AsNoTracking()
                .Where(o => o.OrganisationRoleId == roleId)
                .ToListAsync();
        }
    }
}
