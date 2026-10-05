using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForAccount
{
    public record GetEmailCategoriesForAccountQuery(Guid EmailAccountId) :IQuery <IEnumerable<GetEmailCategoryDto>>
    {
        
    }
}