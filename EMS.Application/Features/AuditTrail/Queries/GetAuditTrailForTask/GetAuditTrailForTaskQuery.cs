using EMS.Application.Dtos.TaskAuditTrail;
using MediatR;

namespace EMS.Application.Features.AuditTrail.Queries.GetAuditTrailForTask
{
    public record GetAuditTrailForTaskQuery(Guid TaskId) : IRequest<IEnumerable<GetTaskAuditTrailDto>>
    {
    }
}