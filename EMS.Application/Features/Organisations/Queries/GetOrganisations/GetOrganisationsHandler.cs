using AutoMapper;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Organisations.Queries.GetOrganisations
{
    public class GetOrganisationsHandler(IOrganisationRepository organisationRepository,IMapper mapper) : IRequestHandler<GetOrganisationsQuery, IEnumerable<OrganisationDto>>
    {
        public async Task<IEnumerable<OrganisationDto>> Handle(GetOrganisationsQuery request, CancellationToken cancellationToken)
        {
            var organisations= await organisationRepository.GetAllAsync();
            return mapper.Map<IEnumerable<OrganisationDto>>(organisations);
        }
    }
}
