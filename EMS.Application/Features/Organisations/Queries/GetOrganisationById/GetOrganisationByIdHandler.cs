using AutoMapper;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Organisations.Queries.GetOrganisationById
{
    public class GetOrganisationByIdHandler (IOrganisationRepository organisationRepository,IMapper mapper,IUserService userService): IRequestHandler<GetOrganisationByIdQuery,OrganisationDetailsDto>
    {


        public async Task<OrganisationDetailsDto> Handle(GetOrganisationByIdQuery request, CancellationToken cancellationToken)
        {
            var organisation= await organisationRepository.GetByIdAsync(request.OrganisationId);
            var userDto = await userService.GetUserByIdAsync(organisation.OwnerId);
            var organisationDto = mapper.Map<OrganisationDetailsDto>(organisation);
            organisationDto.Owner = userDto;
            return organisationDto;
        }
    }
}
