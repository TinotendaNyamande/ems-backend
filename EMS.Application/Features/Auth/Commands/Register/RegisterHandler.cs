using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler(IAuthService authService) : ICommandHandler<RegisterCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerDto = new RegisterUserDto(request.FirstName, request.LastName, request.Email, request.Password,request.Role);

            return await authService.RegisterAsync(registerDto);
        }
    }
}
