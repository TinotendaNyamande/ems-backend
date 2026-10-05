using EMS.Application.Abstractions;

namespace EMS.Application.Features.Auth.Commands.Logout
{
    public record LogoutCommand(string? RefreshToken) :ICommand 
    {
    }
}
