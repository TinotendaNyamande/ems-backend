using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;

namespace EMS.Application.Features.UsersManagement.Queries.GetUserById
{
    public class GetUserByIdQuery(string userId) : ICommand<UserDto>
    {
        public string UserId { get; init; } = userId;
    }
}
