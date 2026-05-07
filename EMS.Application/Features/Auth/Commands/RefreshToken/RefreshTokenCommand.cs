using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string refreshToken) : IRequest<AuthResponseDto>
    {
        public string RefreshToken { get; init; } = refreshToken;
    }
}
