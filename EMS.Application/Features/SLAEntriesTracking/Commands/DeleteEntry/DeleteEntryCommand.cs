namespace EMS.Application.Features.SLAEntriesTracking.Commands.DeleteEntry
{
    using MediatR;

    public record DeleteEntryCommand(Guid Id) : IRequest
    {
    }


}