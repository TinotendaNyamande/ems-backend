using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IOrganisationRepository
    {
        public  Task<Organisation> GetByIdAsync(Guid id);
        public Task DeleteAsync(Guid id);
        public Task CreateAsync(Organisation organisation,CancellationToken cancellationToken);
        public Task RenameOrganisationAsync(Guid id,string newName);
        public Task ChangeOwnerAsync(Guid id, string newOwnerId);
        public Task<IEnumerable<Organisation>> GetAllAsync();

    }
}
