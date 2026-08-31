using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Queries.GetAllUsers
{
    public record GetAllUsersQuery():IRequest<IEnumerable<UserDto>>
    {
    }
}
