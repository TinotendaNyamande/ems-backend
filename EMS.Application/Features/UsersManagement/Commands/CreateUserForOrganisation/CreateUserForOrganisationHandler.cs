using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.CreateUserForOrganisation
{
    public class CreateUserForOrganisationHandler(IAuthService authService,IUserService userService) : IRequestHandler<CreateUserForOrganisationCommand>
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

            var userId = await authService.CreateUserForOrganisationAsync(request.Role, createUserDto);
            await userService.AddUserToOrganisationAsync(request.OrganisationId, userId);
        }
    }
}
