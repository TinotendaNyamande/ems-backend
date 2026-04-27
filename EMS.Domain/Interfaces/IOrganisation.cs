using EMS.Domain.Models;

namespace EMS.Domain.Interfaces
{
    public interface IOrganisation
    {
        public Task SaveChangesAsync();
        public  Task<Organisation> GetByIdAsync(Guid id);
        public Task DeleteAsync(Guid id);
        public Task CreateAsync(Organisation organisation);

    }
}
