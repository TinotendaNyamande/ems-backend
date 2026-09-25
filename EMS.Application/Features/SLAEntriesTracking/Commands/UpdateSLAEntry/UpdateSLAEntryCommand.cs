using EMS.Application.Abstractions;
using EMS.Domain.Enums;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry
{
    public record UpdateSLAEntryCommand(Guid Id, DateTime EndTime, SLAEntryStatus Status) : ICommand;
}