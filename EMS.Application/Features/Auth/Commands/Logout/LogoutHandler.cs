using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.Logout
{
    public class LogoutHandler(IAuthService authService) : IRequestHandler<LogoutCommand>
    {
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await authService.LogoutAsync(request.RefreshToken);
        }
    }
}
