using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.RenameEmailCategory
{
    public record RenameEmailCategoryCommand(Guid Id,string NewName):IRequest;
}