using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;

namespace EMS.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string? RefreshToken) : ICommand<AuthResponseDto>
    {
    }
}
