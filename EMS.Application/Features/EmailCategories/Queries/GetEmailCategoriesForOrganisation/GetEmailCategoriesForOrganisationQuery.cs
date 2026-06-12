using EMS.Application.Dtos.EmailCategories;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForOrganisation
{
    public record GetEmailCategoriesForOrganisationQuery(Guid OrganisationId) : IRequest<IEnumerable<GetEmailCategoriesDto>>
    {
        
    }
}