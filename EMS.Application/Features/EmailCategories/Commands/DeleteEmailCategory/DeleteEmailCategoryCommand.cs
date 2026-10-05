using EMS.Application.Abstractions;

namespace EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory
{
    public record DeleteEmailCategoryCommand(Guid CategoryId,Guid NewCategoryId) : ICommand
    {
        
    }
}