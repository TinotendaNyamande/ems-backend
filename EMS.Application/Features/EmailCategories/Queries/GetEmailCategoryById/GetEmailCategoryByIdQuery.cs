using EMS.Application.Dtos.EmailCategories;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoryById
{
    public record GetEmailCategoryByIdQuery(Guid Id) : IRequest<GetEmailCategoryDto>
    {
        
    }
}