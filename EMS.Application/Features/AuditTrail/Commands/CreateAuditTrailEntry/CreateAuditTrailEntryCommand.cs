using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry
{
    public record CreateAuditTrailEntryCommand(Guid TaskId,string UserId,string Comments):ICommand<TaskAuditTrail>;
}