using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using EMS.Application.Interfaces;


namespace EMS.Infrastructure.Repository
{
    internal class OrganisationRepository(ApplicationDbContext context, ILogger<OrganisationRepository> logger, UserManager<ApplicationUser> userManager) : IOrganisationRepository
    {
        public async Task ChangeOwnerAsync(Guid id, string newOwnerId)
        {
            logger.LogInformation("Attempting changing owner to {newOwnerId}", newOwnerId);
            var organisation = await context.Organisations
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Organisation", id);
            //move to application layer
            var user = await userManager.FindByIdAsync(newOwnerId) ?? throw new ResourceNotFoundException("User", newOwnerId);
            if(user.OrganisationId != id)
            {
                logger.LogError("Failed to change owner for organisation : {id}", id);
                logger.LogError("New owner belongs to different organisation: {OrganisationId}",user.OrganisationId);
                throw new InvalidOperationException("New owner must belong to the same organisation");
            }
            organisation.ChangeOwner(newOwnerId);
            await context.SaveChangesAsync();
            logger.LogInformation("Chnage of organisation owner successfull");

        }

        public async Task CreateAsync(Organisation organisation,CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(organisation);

            logger.LogInformation("Creating organisation,Organisation name : {Name} , OwnerId:{OwnerId}", organisation.Name, organisation.OwnerId);
            context.Add(organisation);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Organisation {Name} created successfully", organisation.Name);
        }

        public async Task DeleteAsync(Guid id)
        {
            logger.LogInformation("Delete:Organisation Id {id}", id);
            var affectedRows = await context.Organisations.Where(o => o.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                throw new ResourceNotFoundException("Organisation", id);
            }
            logger.LogInformation("Deleting organisation with id :{id} succcssfully", id);
            logger.LogInformation("Deleted {affectedRows}",affectedRows);
        }

        public async Task<IEnumerable<Organisation>> GetAllAsync()
        {
            return await context.Organisations.AsNoTracking().ToListAsync();
        }

        public async Task<Organisation> GetByIdAsync(Guid id)
        {
            logger.LogInformation("Attempting to find organisation Id: {id}",id);
            return await context.Organisations
                .AsNoTracking()
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException("Organisation", id);
        }

        public async Task RenameOrganisationAsync(Guid id, string newName)
        {
            logger.LogInformation("Attempting to rename organisation :{id} to {newName}",id,newName);
            var organisation = await context.Organisations
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Organisation", id);

            organisation.Rename(newName);
            await context.SaveChangesAsync();
            logger.LogInformation("Change of organisation name successfull");
        }

    }
}
