using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.AssignEmailToUser
{
    public record AssignEmailToUserCommand(Guid EmailId,Guid EmailCategoryId):ICommand;
}