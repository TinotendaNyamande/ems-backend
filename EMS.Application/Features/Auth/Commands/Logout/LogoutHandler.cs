using EMS.Application.Abstractions;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.Auth.Commands.Logout
{
    public class LogoutHandler(IAuthService authService) : ICommandHandler<LogoutCommand>
    {
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                throw new ArgumentException("Refresh token cannot be null or empty.", nameof(request.RefreshToken));
            }
            await authService.LogoutAsync(request.RefreshToken);
        }
    }
}
