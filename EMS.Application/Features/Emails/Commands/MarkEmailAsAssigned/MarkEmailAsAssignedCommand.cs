using EMS.Application.Abstractions;

namespace EMS.Application.Features.Emails.Commands.MarkEmailAsAssigned
{
    public record MarkEmailAsAssignedCommand(Guid EmailId):ICommand;
}