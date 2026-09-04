using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.UpdateEntry
{
    public record UpdateEntryCommand(Guid Id, DateTime EndTime, string Comments, string Status) : IRequest;
}