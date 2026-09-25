using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForAccount
{
    public record GetEmailCategoriesForAccountQuery(Guid EmailAccountId) :ICommand <IEnumerable<GetEmailCategoryDto>>
    {
        
    }
}