using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.CreateUser
{
    public record CreateUserCommand(string FirstName, string LastName, string Email, string Password,string Role):IRequest<UserDto>
    {
    }
}
