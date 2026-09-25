using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory
{
    public record DeleteEmailCategoryCommand(Guid CategoryId,Guid NewCategoryId) : ICommand
    {
        
    }
}