using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.EditEmailCategory
{
    public record EditEmailCategoryCommand(Guid Id, string NewName, int SlaHours) : IRequest;
}