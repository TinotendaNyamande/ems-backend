namespace EMS.Application.Features.SLAEntriesTracking.Commands.DeleteSLAEntry
{
    using EMS.Application.Abstractions;
    public record DeleteSLAEntryCommand(Guid Id) : ICommand
    {
    }


}