using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForAccount
{
    public class GetEmailCategoriesForAccountHandler(IEmailCategoryRepository emailCategoryRepository,IMapper mapper) : IQueryHandler<GetEmailCategoriesForAccountQuery, IEnumerable<GetEmailCategoryDto>>
    {
        public async Task<IEnumerable<GetEmailCategoryDto>> Handle(GetEmailCategoriesForAccountQuery request, CancellationToken cancellationToken)
        {
            var emailCategories = await emailCategoryRepository.GetEmailCategoriesByEmailAccountAsync(request.EmailAccountId);
            return mapper.Map<IEnumerable<GetEmailCategoryDto>>(emailCategories);
        }
    }
}