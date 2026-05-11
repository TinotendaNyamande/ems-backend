using AutoMapper;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.AssignUserRole
{
    public class AssignUserRoleHandler(IOrganisationUserRoleRepository organisationUserRoleRepository, IUserService userService, IMapper mapper) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            //check if users exists
            _= await userService.GetUserByIdAsync(request.UserId);
            // Remove existing roles from the user
            await organisationUserRoleRepository.RemoveRolesFromUserAsync(request.UserId);
            // Assign the new role to the user
            var organisationUserRole = mapper.Map<OrganisationUserRole>(request);
            await organisationUserRoleRepository.AddRoleToUserAsync(organisationUserRole);

        }
    }
   
    
}
