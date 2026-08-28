using AutoMapper;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForAccount
{
    public class GetEmailCategoriesForAccountHandler(IEmailCategoryRepository emailCategoryRepository,IMapper mapper) : IRequestHandler<GetEmailCategoriesForAccountQuery, IEnumerable<GetEmailCategoryDto>>
    {
        public async Task<IEnumerable<GetEmailCategoryDto>> Handle(GetEmailCategoriesForAccountQuery request, CancellationToken cancellationToken)
        {
            var emailCategories = await emailCategoryRepository.GetEmailCategoriesAsync(request.EmailAccountId);
            return mapper.Map<IEnumerable<GetEmailCategoryDto>>(emailCategories);
        }
    }
}