using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string? RefreshToken) : IRequest<AuthResponseDto>
    {
    }
}
