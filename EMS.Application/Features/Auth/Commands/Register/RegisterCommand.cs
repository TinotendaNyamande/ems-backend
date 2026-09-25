using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(string FirstName, string LastName, string Email, string Password,string Role) :ICommand <AuthResponseDto>
    {

    }
}
