using EMS.Application.Dtos.SLATracking;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface ISLATrackingRepository
    {
        Task<SLATracking> GetByIdAsync(Guid id);
        Task<IEnumerable<SLATracking>> GetByEmailTaskIdAsync(Guid emailTaskId);
        Task<SLATracking> AddAsync(SLATracking slaTracking);
        Task UpdateAsync(Guid id,UpdateSLAEntryDto slaTracking);
        Task DeleteAsync(Guid id);
        Task<SLATracking> GetCurrentEntryForTaskAsync(Guid emailTaskId);
        Task<IEnumerable<SLATracking>> GetEntriesByUserIdAsync(string userId);
    }
}