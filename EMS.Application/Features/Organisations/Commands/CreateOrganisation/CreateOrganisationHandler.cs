using AutoMapper;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;


namespace EMS.Application.Features.Organisations.Commands.CreateOrganisation
{
    public class CreateOrganisationHandler(
        IMapper mapper, 
        IOrganisationRepository organisationRepository,
        IUserService userService,
        IRolesRepository rolesRepository,
        IOrganisationUserRoleRepository organisationUserRoleRepository
        ) : IRequestHandler<CreateOrganisationCommand,OrganisationDto>
    {
        public async Task<OrganisationDto> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            //create organisation object
            var organisation = mapper.Map<Organisation>(request);
            await organisationRepository.CreateAsync(organisation,cancellationToken);

            //create organisation defalut permissions
            await rolesRepository.CreateDefaultRolesAndPermissionsAsync(organisation.Id);

            //get created organisation owner role
            var organisationOwnerRole = await rolesRepository.GetRoleByNameAsync("Owner", organisation.Id);

            
            var OrganisationUserRole = new OrganisationUserRole(request.OwnerId,organisationOwnerRole.Id);

            //remove existing roles of the user if any
            await organisationUserRoleRepository.RemoveRolesFromUserAsync(request.OwnerId);

            //assign owner role to the user
            await organisationUserRoleRepository.AddRoleToUserAsync(OrganisationUserRole);

            //add user to the organisation
            await userService.AddUserToOrganisationAsync(organisation.Id, organisation.OwnerId);
            return mapper.Map<OrganisationDto>(organisation);
        }
    }
}
