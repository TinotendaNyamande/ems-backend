using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Queries.GetUsersForOrganisationQuery
{
    public class GetUsersForOrganisationHandler(IUserService userService) : IRequestHandler<GetUsersForOrganisationQuery, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetUsersForOrganisationQuery request, CancellationToken cancellationToken)
        {
            return await userService.GetUsersInOrganisationAsync(request.OrganisationId);
        }
    }
}
