using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;

namespace EMS.Application.Features.EmailCategories.Queries.GetCategoryByName
{
    public record GetCategoryByNameQuery(Guid EmailAccountId,string CategoryName) : IQuery<GetEmailCategoryDto>
    {
    }
}