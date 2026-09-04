using EMS.Application.Dtos.SLATracking;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.CreateEntry
{
    public record CreateEntryCommand (Guid EmailTaskId, DateTime StartTime, DateTime? EndTime, string Comments): IRequest<SLATrackingEntryDto>{}
}