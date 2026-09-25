using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;

namespace EMS.Application.Features.UsersManagement.Commands.CreateUser
{
    public record CreateUserCommand(string FirstName, string LastName, string Email, string Password,string Role):ICommand<UserDto>
    {
    }
}
