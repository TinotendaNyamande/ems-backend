using EMS.Application.Dtos.RolesAndPermissions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class RolePermissionsRepository(ApplicationDbContext context, ILogger<RolePermissionsRepository> logger) : IRolePermissionsRepository
    {
        public async Task CreatePermissionAsync(OrganisationRolePermission permission)
        {
            context.Add(permission);
            await context.SaveChangesAsync();
        }

        public async Task EditPermissionsAsync(Guid permissionId, EditPermissionDto editPermissionDto)
        {
            logger.LogInformation("Editing permission with id {PermissionId} to {IsAllowed}", permissionId, editPermissionDto.IsAllowed);
            var permission = await context.OrganisationRolePermissions
              .Where(p => p.Id == permissionId)
              .FirstOrDefaultAsync()
              ?? throw new ResourceNotFoundException("Permission", permissionId);
              logger.LogInformation("Current permission state is {IsAllowed}", permission.IsAllowed);
              logger.LogInformation("Editing permission with id {PermissionId}", permission.Id);
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
