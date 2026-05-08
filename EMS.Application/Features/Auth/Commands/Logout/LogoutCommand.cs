using MediatR;

namespace EMS.Application.Features.Auth.Commands.Logout
{
    public record LogoutCommand(string? RefreshToken) : IRequest
    {
    }
}
