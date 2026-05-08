using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>
    {

    }
}
