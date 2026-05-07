using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(string firstName, string lastName, string email, string password) : IRequest<AuthResponseDto>
    {
        public string FirstName { get; init; } = firstName;
        public string LastName { get; init; } = lastName;
        public string Email { get; init; } = email;
        public string Password { get; init; } = password;
    }
}
