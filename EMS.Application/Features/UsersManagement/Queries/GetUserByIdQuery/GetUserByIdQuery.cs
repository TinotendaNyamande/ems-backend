using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Queries.GetUserByIdQuery
{
    public class GetUserByIdQuery(string userId) : IRequest<UserDto>
    {
        public string UserId { get; init; } = userId;
    }
}
