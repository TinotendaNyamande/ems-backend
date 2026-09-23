using EMS.Application.Dtos.SLATracking;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface ISLATrackingRepository
    {
        Task<GetSLATrackingDto> GetByIdAsync(Guid id);
        Task<IEnumerable<GetSLATrackingDto>> GetByEmailTaskIdAsync(Guid emailTaskId);
        Task<SLATracking> CreateSLAEntryAsync(SLATracking slaTracking);
        Task StopTimerAsync(Guid id,UpdateSLAEntryDto slaTracking);
        Task DeleteAsync(Guid id);
        Task<GetSLATrackingDto> GetCurrentEntryForTaskAsync(Guid emailTaskId);
        Task<IEnumerable<GetSLATrackingDto>> GetEntriesByUserIdAsync(string userId);
    }
}