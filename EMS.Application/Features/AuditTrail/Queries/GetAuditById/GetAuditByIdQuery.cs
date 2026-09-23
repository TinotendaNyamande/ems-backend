using EMS.Application.Dtos.TaskAuditTrail;
using MediatR;

namespace EMS.Application.Features.AuditTrail.Queries.GetAuditById
{
    public record GetAuditByIdQuery(Guid Id):IRequest<GetTaskAuditTrailDto>;
}