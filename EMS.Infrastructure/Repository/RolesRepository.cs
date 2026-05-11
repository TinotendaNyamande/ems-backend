using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class RolesRepository (ApplicationDbContext context): IRolesRepository
    {
        public async Task CreateRoleAsync(OrganisationRole organisationRole)
        {
            context.Add(organisationRole);
            await context.SaveChangesAsync();
        }

        public async Task<OrganisationRole> GetRoleByIdAsync(Guid roleId)
        {
            return await context.OrganisationRoles
                .AsNoTracking()
                .Where(r=>r.Id== roleId)
                .FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException("Role", roleId);
        }

        public async Task<IEnumerable<OrganisationRole>> GetRolesForOrganisationAsync(Guid organisationId)
        {
            return await context.OrganisationRoles
                .AsNoTracking()
                .Where(r => r.OrganisationId == organisationId)
                .ToListAsync();
        }
        public async Task<IEnumerable<OrganisationUserRole>> GetRoleByUserIdAsync(string userId)
        {
            return await context.OrganisationUserRoles
               .AsNoTracking()
               .Where(r => r.UserId == userId)
               .ToListAsync();
        }
        public async Task CreateDefaultRolesAndPermissionsAsync(Guid organisationId)
        {
            _ = await context.Organisations.FindAsync(organisationId)
                ?? throw new ResourceNotFoundException("Organisation", organisationId);

            var defaultRoles = new List<OrganisationRole>
            {
                BuildRole(organisationId, "Owner", new[]
                {
                    PermissionKeys.JoinRequestsView,
                    PermissionKeys.JoinRequestsApprove,
                    PermissionKeys.TasksCreate,
                    PermissionKeys.TasksDelete,
                    PermissionKeys.TasksEdit,
                    PermissionKeys.TasksView,
                    PermissionKeys.MailBoxesCreate,
                    PermissionKeys.MailBoxesDelete,
                    PermissionKeys.MailBoxesEdit,
                    PermissionKeys.MailBoxesView,
                    PermissionKeys.OrganisationView,
                    PermissionKeys.OrganisationEdit,
                    PermissionKeys.OrganisationDelete,
                    PermissionKeys.UsersCreate,
                    PermissionKeys.UsersView,
                    PermissionKeys.UsersDelete,
                    PermissionKeys.UsersEdit,
                    PermissionKeys.PermissionsView,
                    PermissionKeys.PermissionsEdit,
           
                }),
                BuildRole(organisationId, "Manager", new[]
                {
                   PermissionKeys.JoinRequestsView,
                    PermissionKeys.JoinRequestsApprove,
                    PermissionKeys.TasksCreate,
                    PermissionKeys.TasksEdit,
                    PermissionKeys.TasksView,
                    PermissionKeys.MailBoxesCreate,
                    PermissionKeys.MailBoxesEdit,
                    PermissionKeys.MailBoxesView,
                    PermissionKeys.OrganisationView,
                    PermissionKeys.OrganisationEdit,
                    PermissionKeys.UsersCreate,
                    PermissionKeys.UsersView,
                    PermissionKeys.UsersEdit,
                    PermissionKeys.PermissionsView,
                    PermissionKeys.PermissionsEdit,
                }),
                BuildRole(organisationId, "Supervisor", new[]
                {
                   PermissionKeys.JoinRequestsView,
                    PermissionKeys.JoinRequestsApprove,
                    PermissionKeys.TasksCreate,
                    PermissionKeys.TasksEdit,
                    PermissionKeys.TasksView,
                    PermissionKeys.MailBoxesCreate,
                    PermissionKeys.MailBoxesEdit,
                    PermissionKeys.MailBoxesView,
                    PermissionKeys.OrganisationView,
                    PermissionKeys.OrganisationEdit,
                    PermissionKeys.UsersCreate,
                    PermissionKeys.UsersView,
                    PermissionKeys.UsersEdit,
                    PermissionKeys.PermissionsView,
                }),
                BuildRole(organisationId, "User", new[]
                {
                    PermissionKeys.TasksEdit,
                    PermissionKeys.TasksView,
                    PermissionKeys.MailBoxesView,
                    PermissionKeys.OrganisationView,
                    PermissionKeys.UsersView,
                })
            };

            context.OrganisationRoles.AddRange(defaultRoles);
            await context.SaveChangesAsync();
        }

        private static OrganisationRole BuildRole(Guid organisationId, string roleName,IEnumerable<string> allowedPermissions)
        {
            var allowedSet = allowedPermissions.ToHashSet(StringComparer.OrdinalIgnoreCase);

            return new OrganisationRole
            {
                OrganisationId = organisationId,
                RoleName = roleName,
                Permissions = PermissionCatalog.All
                    .Select(permission => new OrganisationRolePermission
                    {
                        PermissionKey = permission.Key,
                        IsAllowed = allowedSet.Contains(permission.Key)
                    })
                    .ToList()
            };
        }

        public async Task<bool> CanAccess(string permission, string userId)
        {
            var user = await context.Users.FindAsync(userId) ?? throw new ResourceNotFoundException("User", userId);
            var userRole = await context.OrganisationUserRoles.Where(p => p.UserId == userId).FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException("User Role", userId);
            var hasPermission = await context.OrganisationRolePermissions
                .Where(p => p.OrganisationRoleId == userRole.RoleId && p.PermissionKey == permission && p.IsAllowed)
                .AnyAsync();

            return hasPermission;

        }

        public async Task<OrganisationRole> GetRoleByNameAsync(string roleName, Guid organisationId)
        {
            return await context.OrganisationRoles
                .AsNoTracking()
                .Where(r => r.OrganisationId == organisationId && r.RoleName == roleName)
                .FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException("Role", roleName);
        }
    }
}
