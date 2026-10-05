using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.UsersManagement.Commands.CreateUser
{
    public class CreateUserHandler(
        IAuthService authService
        ) : ICommandHandler<CreateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var createUserDto = new RegisterUserDto(request.FirstName,request.LastName,request.Email,request.Password,request.Role);

            if (request.Role != "Admin" && request.Role != "Supervisor" && request.Role != "Member")
            {
    
                throw new ArgumentException("Invalid role specified. Role must be either 'Admin', 'Supervisor', or 'Member'.");
            }

            var user = await authService.CreateUserAsync(request.Role, createUserDto);
            return user;
        }
    }
}
