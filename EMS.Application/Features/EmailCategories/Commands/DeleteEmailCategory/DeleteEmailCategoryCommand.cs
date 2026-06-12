using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory
{
    public record DeleteEmailCategoryCommand(Guid Id,Guid? NewCategoryId=null) : IRequest
    {
        
    }
}