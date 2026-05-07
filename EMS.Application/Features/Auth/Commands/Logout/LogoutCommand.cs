using MediatR;

namespace EMS.Application.Features.Auth.Commands.Logout
{
    public record LogoutCommand(string refreshToken) : IRequest
    {
        public string RefreshToken { get; init; } = refreshToken;
    }
}
