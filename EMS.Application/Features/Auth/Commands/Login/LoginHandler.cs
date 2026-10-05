using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.Auth.Commands.Login
{
    public class LoginHandler(IAuthService authService) : ICommandHandler<LoginCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginDto = new LoginUserDto(request.Email, request.Password);

            return await authService.LoginAsync(loginDto);
        }
    }
}
