using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.ChangeEmailCategory
{
    public record ChangeEmailCategoryCommand(Guid EmailId,Guid NewCategoryId):ICommand;
}