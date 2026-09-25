using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;

namespace EMS.Application.Features.UsersManagement.Queries.GetAllUsers
{
    public record GetAllUsersQuery():ICommand<IEnumerable<UserDto>>
    {
    }
}
