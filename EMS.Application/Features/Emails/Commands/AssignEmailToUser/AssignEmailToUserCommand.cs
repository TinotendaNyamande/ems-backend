using EMS.Application.Abstractions;

namespace EMS.Application.Features.Emails.Commands.AssignEmailToUser
{
    public record AssignEmailToUserCommand(Guid EmailId,Guid EmailCategoryId):ICommand;
}