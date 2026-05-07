using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string email, string password) : IRequest<AuthResponseDto>
    {
        public string Email { get; init; } = email;
        public string Password { get; init; } = password;
    }
}
