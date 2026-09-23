using EMS.Application.Dtos.TaskAuditTrail;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface ITaskAuditRepository
    {
        Task<TaskAuditTrail> CreateTaskAuditTrailAsync(TaskAuditTrail taskAuditTrail);
        Task<IEnumerable<GetTaskAuditTrailDto>> GetTaskAuditTrail (Guid emailTaskId);
        Task<GetTaskAuditTrailDto> GetAuditTrailEntryById(Guid id);
        

    }
}