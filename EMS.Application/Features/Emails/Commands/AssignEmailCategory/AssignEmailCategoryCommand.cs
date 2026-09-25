using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.AssignEmailCategory
{
    public record AssignEmailCategoryCommand(Guid EmailId, Guid CategoryId) :ICommand ;
}