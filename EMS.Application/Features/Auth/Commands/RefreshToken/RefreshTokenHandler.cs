using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenHandler(IAuthService authService) : ICommandHandler<RefreshTokenCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.RefreshToken))
            {
                throw new ArgumentException("Refresh token cannot be null or empty.", nameof(request.RefreshToken));
            }
            return await authService.RefreshTokenAsync(request.RefreshToken);
        }
    }
}
