using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using EMS.Domain.Exceptions;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.CreateUserForOrganisation
{
    public class CreateUserForOrganisationHandler(
        IAuthService authService,
        IUserService userService
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



            var userId = await authService.CreateUserForOrganisationAsync(request.Role, createUserDto);
        }
    }
}
