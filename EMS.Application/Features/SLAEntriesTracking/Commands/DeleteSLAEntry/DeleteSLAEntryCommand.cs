namespace EMS.Application.Features.SLAEntriesTracking.Commands.DeleteSLAEntry
{
    using MediatR;

    public record DeleteEntryCommand(Guid Id) : IRequest
    {
    }


}