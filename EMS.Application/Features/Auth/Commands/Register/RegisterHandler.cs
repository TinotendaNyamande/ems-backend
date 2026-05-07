using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler(IAuthService authService) : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerDto = new RegisterUserDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };

            return await authService.RegisterAsync(registerDto);
        }
    }
}
