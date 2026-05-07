using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenHandler(IAuthService authService) : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await authService.RefreshTokenAsync(request.RefreshToken);
        }
    }
}
