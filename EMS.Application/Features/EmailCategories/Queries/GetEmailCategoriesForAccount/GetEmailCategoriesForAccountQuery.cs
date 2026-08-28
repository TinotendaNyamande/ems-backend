using EMS.Application.Dtos.EmailCategories;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForAccount
{
    public record GetEmailCategoriesForAccountQuery(Guid EmailAccountId) : IRequest<IEnumerable<GetEmailCategoryDto>>
    {
        
    }
}