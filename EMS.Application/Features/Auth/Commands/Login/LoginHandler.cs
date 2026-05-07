using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.Login
{
    public class LoginHandler(IAuthService authService) : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginDto = new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password
            };

            return await authService.LoginAsync(loginDto);
        }
    }
}
