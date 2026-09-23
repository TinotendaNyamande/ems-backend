using MediatR;

namespace EMS.Application.Features.Emails.Commands.MarkEmailAsAssigned
{
    public record MarkEmailAsAssignedCommand(Guid EmailId):IRequest;
}