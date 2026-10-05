using EMS.Application.Abstractions;
using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.UsersManagement.Queries.GetUserById
{
    public class GetUserByIdHandler(IUserService userService) : IQueryHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userService.GetUserByIdAsync(request.UserId);
        }
    }
}
