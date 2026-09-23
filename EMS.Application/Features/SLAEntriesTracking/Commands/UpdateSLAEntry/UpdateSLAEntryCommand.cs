using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry
{
    public record UpdateSLAEntryCommand(Guid Id, DateTime EndTime, string Comments, string Status) : IRequest;
}