using EMS.Application.Abstractions;

namespace EMS.Application.Features.Emails.Commands.ChangeEmailCategory
{
    public record ChangeEmailCategoryCommand(Guid EmailId,Guid NewCategoryId):ICommand;
}