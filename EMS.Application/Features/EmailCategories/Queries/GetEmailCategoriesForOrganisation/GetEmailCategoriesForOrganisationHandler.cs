using AutoMapper;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForOrganisation
{
    public class GetEmailCategoriesForOrganisationHandler(IEmailCategoryRepository emailCategoryRepository,IMapper mapper) : IRequestHandler<GetEmailCategoriesForOrganisationQuery, IEnumerable<GetEmailCategoriesDto>>
    {
        public async Task<IEnumerable<GetEmailCategoriesDto>> Handle(GetEmailCategoriesForOrganisationQuery request, CancellationToken cancellationToken)
        {
            var emailCategories = await emailCategoryRepository.GetEmailCategoriesAsync(request.OrganisationId);
            return mapper.Map<IEnumerable<GetEmailCategoriesDto>>(emailCategories);
        }
    }
}