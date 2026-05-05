using EMS.Application.Dtos.Organisation;
using MediatR;

namespace EMS.Application.Features.Organisations.Queries.GetOrganisations
{
    public class GetOrganisationsQuery():IRequest<IEnumerable<OrganisationDto>>
    {
        
    }
}
