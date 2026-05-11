using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;


namespace EMS.Infrastructure.Repository
{
    internal class OrganisationUserRoleRepository(ApplicationDbContext context) : IOrganisationUserRoleRepository
    {
        public async Task AddRoleToUserAsync(OrganisationUserRole organisationUserRole)
        {
            context.Add(organisationUserRole);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrganisationUserRole>> GetRolesByUserIdAsync(string userId)
        {
            return await context.OrganisationUserRoles
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .ToListAsync();
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
    }
}
