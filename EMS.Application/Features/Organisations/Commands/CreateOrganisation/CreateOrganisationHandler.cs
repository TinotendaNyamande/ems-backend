using AutoMapper;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;


namespace EMS.Application.Features.Organisations.Commands.CreateOrganisation
{
    public class CreateOrganisationHandler(IMapper mapper, IOrganisationRepository organisationRepository,IUserService userService) : IRequestHandler<CreateOrganisationCommand,OrganisationDto>
    {
        public async Task<OrganisationDto> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            var organisation = mapper.Map<Organisation>(request);
            await organisationRepository.CreateAsync(organisation,cancellationToken);
            await userService.AssignRoleAsync("Owner", organisation.OwnerId);
            await userService.AddUserToCompanyAsync(organisation.Id, organisation.OwnerId);
            return mapper.Map<OrganisationDto>(organisation);
        }
    }
}
