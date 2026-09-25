using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.CreateUser
{
    public class CreateUserHandler(
        IAuthService authService
        ) : ICommandHandler<CreateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var createUserDto = new RegisterUserDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };

            if (request.Role != "Admin" && request.Role != "Supervisor" && request.Role != "Member")
            {
    
                throw new ArgumentException("Invalid role specified. Role must be either 'Admin', 'Supervisor', or 'Member'.");
            }

            var user = await authService.CreateUserAsync(request.Role, createUserDto);
            return user;
        }
    }
}
