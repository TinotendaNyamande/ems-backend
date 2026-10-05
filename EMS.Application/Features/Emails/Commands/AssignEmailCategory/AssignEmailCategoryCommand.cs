using EMS.Application.Abstractions;

namespace EMS.Application.Features.Emails.Commands.AssignEmailCategory
{
    public record AssignEmailCategoryCommand(Guid EmailId, Guid CategoryId) :ICommand ;
}