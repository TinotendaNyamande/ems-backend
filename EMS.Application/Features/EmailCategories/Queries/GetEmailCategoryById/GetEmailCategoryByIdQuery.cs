using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoryById
{
    public record GetEmailCategoryByIdQuery(Guid Id) : IQuery<GetEmailCategoryDto>
    {
        
    }
}