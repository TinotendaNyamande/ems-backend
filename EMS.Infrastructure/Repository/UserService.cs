using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using EMS.Infrastructure.persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Projects.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EMS.Infrastructure.Repository
{
    internal class UserService(
            UserManager<ApplicationUser> userManager,
            ILogger<AuthService> logger,
            ApplicationDbContext context,
            IOrganisationUserRoleRepository organisationUserRoleRepository
            ) : IUserService
    {

        public async Task AddUserToOrganisationAsync(Guid organisationId, string userId)
        {
            logger.LogInformation("Attempting to add user : {userId} to organisation {companyId}", userId, organisationId);
            var user = await userManager.FindByIdAsync(userId) ?? throw new ResourceNotFoundException($"User", userId);
            var organisation = await context.Organisations.FindAsync(organisationId)
                ?? throw new ResourceNotFoundException($"Organisation", organisationId);
            user.AddOrganisationIdToUser(organisationId);
            await context.SaveChangesAsync();
            logger.LogInformation("Successfully added user : {userId} to organisation {companyId}", userId, organisationId);
        }

        public async Task<Guid?> CheckIfUserBelongsToAnyOrganisationAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new ResourceNotFoundException($"User", userId);
            return user.OrganisationId;
        }

        public async Task<Guid?> CheckIfUserIsOrganisationOwner(string userId)
        {
            var org = await context.Organisations.AsNoTracking().Where(o => o.OwnerId == userId).FirstOrDefaultAsync();
            return org?.Id;
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId)
                ?? throw new ResourceNotFoundException($"User", userId);
            var role = await organisationUserRoleRepository.GetRoleByUserIdAsync(userId);
            return new UserDto
            {
                Id = user.Id,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
                OrganisationId=user.OrganisationId,
                Role = role?.Role.RoleName
            };
        }



        public async Task<IEnumerable<UserDto>> GetUsersInOrganisationAsync(Guid organisationId)
        {
            //var query = from users in context.Users.AsNoTracking()
            //            where users.OrganisationId == organisationId
            //            join userroles in context.OrganisationUserRoles.AsNoTracking()
            //            on users.Id equals userroles.UserId
            //            join roles in context.OrganisationRoles.AsNoTracking()
            //            on userroles.RoleId equals roles.Id
            //            select new UserDto
            //            {
            //                Id = users.Id,
            //                FirstName = users.FirstName,
            //                LastName = users.LastName,
            //                Email = users.Email,
            //                OrganisationId = users.OrganisationId,
            //                Role = roles.RoleName
            //            };
            var query =
                from users in context.Users.AsNoTracking()
                where users.OrganisationId == organisationId

                join userroles in context.OrganisationUserRoles.AsNoTracking()
                    on users.Id equals userroles.UserId into userRoleGroup

                from userrole in userRoleGroup.DefaultIfEmpty()

                join roles in context.OrganisationRoles.AsNoTracking()
                    on userrole.RoleId equals roles.Id into roleGroup

                from role in roleGroup.DefaultIfEmpty()

                select new UserDto
                {
                    Id = users.Id,
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    Email = users.Email,
                    OrganisationId = users.OrganisationId,
                    Role = role != null ? role.RoleName : null
                };
            return await  query.ToListAsync();
        }
    }
}
