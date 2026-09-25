using EMS.Application.Abstractions;
using EMS.Application.Dtos.TaskAuditTrail;

namespace EMS.Application.Features.AuditTrail.Queries.GetAuditById
{
    public record GetAuditByIdQuery(Guid Id):ICommand<GetTaskAuditTrailDto>;
}