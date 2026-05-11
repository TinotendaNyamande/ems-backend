using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Queries.GetUserByIdQuery
{
    public class GetUserByIdHandler(IUserService userService) : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userService.GetUserByIdAsync(request.UserId);
        }
    }
}
