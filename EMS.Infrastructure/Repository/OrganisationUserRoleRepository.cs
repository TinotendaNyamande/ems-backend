using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace EMS.Infrastructure.Repository
{
    internal class OrganisationUserRoleRepository(ApplicationDbContext context,ILogger<OrganisationUserRoleRepository> logger) : IOrganisationUserRoleRepository
    {
        public async Task AddRoleToUserAsync(OrganisationUserRole organisationUserRole)
        {
            context.Add(organisationUserRole);
            logger.LogInformation("Adding role {RoleId} to user {UserId} ", organisationUserRole.RoleId,organisationUserRole.UserId);
            await context.SaveChangesAsync();
        }

        public async Task RemoveRolesFromUserAsync(string UserId)
        {
            var userRole = await context.OrganisationUserRoles
                .Where(r => r.UserId == UserId)
                .ToListAsync();

            if (userRole != null)
            {
                context.OrganisationUserRoles.RemoveRange(userRole);
                await context.SaveChangesAsync();
            }
        }
        public async Task<OrganisationUserRole?> GetRoleByUserIdAsync(string userId)
        {
            return await context.OrganisationUserRoles
               .AsNoTracking()
               .Where(r => r.UserId == userId)
               .Include(r => r.Role)
               .FirstOrDefaultAsync();
        }
    }
}
