using EMS.Application.Dtos.Auth;
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
                .Include(r => r.Permissions)
                .ToListAsync();
        }

        public async Task CreateDefaultRolesAndPermissionsAsync(Guid organisationId)
        {
            _ = await context.Organisations.FindAsync(organisationId)
                ?? throw new ResourceNotFoundException("Organisation", organisationId);

            var defaultRoles = new List<OrganisationRole>
            {
                new OrganisationRole(organisationId, "Owner", new[]
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
                new OrganisationRole(organisationId, "Manager", new[]
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
                new OrganisationRole(organisationId, "Supervisor", new[]
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
                new OrganisationRole(organisationId, "User", new[]
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

        public async Task<IEnumerable<UserDto>> GetOrganisationUsersByRoleAsync(string roleName, Guid organisationId)
        {
            var query = from user in context.Users
            join userRole in context.OrganisationUserRoles
            on user.Id equals userRole.UserId
            join role in context.OrganisationRoles
            on userRole.RoleId equals role.Id
            where role.RoleName == roleName && user.OrganisationId == organisationId
            select new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                OrganisationId = user.OrganisationId
            };
            return await query.AsNoTracking().ToListAsync();
        }
    }
}
