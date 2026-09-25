namespace EMS.Application.Features.SLAEntriesTracking.Commands.DeleteSLAEntry
{
    using EMS.Application.Abstractions;
    using MediatR;

    public record DeleteSLAEntryCommand(Guid Id) : ICommand
    {
    }


}