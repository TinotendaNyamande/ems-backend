using EMS.Application.Abstractions;
using EMS.Application.Dtos.TaskAuditTrail;

namespace EMS.Application.Features.AuditTrail.Queries.GetAuditTrailForTask
{
    public record GetAuditTrailForTaskQuery(Guid TaskId) : ICommand<IEnumerable<GetTaskAuditTrailDto>>
    {
    }
}