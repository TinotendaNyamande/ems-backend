using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;

namespace EMS.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password) : ICommand<AuthResponseDto>
    {

    }
}
