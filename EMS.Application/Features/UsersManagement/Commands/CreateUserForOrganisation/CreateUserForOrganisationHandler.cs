using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using EMS.Domain.Exceptions;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.CreateUserForOrganisation
{
    public class CreateUserForOrganisationHandler(
        IAuthService authService,
        IUserService userService,
        IJoinRequestsRepository joinRequests,
        IRolesRepository rolesRepository,
        IOrganisationUserRoleRepository organisationUserRoleRepository
        ) : IRequestHandler<CreateUserForOrganisationCommand>
    {
        public async Task Handle(CreateUserForOrganisationCommand request, CancellationToken cancellationToken)
        {

            var createUserDto = new RegisterUserDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };


            var organisationOwnerRole = await rolesRepository.GetRoleByNameAsync(request.Role,request.OrganisationId);

            var userId = await authService.CreateUserForOrganisationAsync(request.Role, createUserDto);
            var OrganisationUserRole = new OrganisationUserRole(userId, organisationOwnerRole.Id);
            //remove existing roles of the user if any
            await organisationUserRoleRepository.RemoveRolesFromUserAsync(userId);

            //assign owner role to the user
            await organisationUserRoleRepository.AddRoleToUserAsync(OrganisationUserRole);
            await userService.AddUserToOrganisationAsync(request.OrganisationId, userId);
        }
    }
}
