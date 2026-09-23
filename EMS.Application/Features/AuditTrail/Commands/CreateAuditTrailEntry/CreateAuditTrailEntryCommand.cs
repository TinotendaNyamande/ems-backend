using EMS.Application.Dtos.TaskAuditTrail;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry
{
    public record CreateAuditTrailEntryCommand(Guid TaskId,string UserId,string Comments):IRequest<GetTaskAuditTrailDto>{};
}