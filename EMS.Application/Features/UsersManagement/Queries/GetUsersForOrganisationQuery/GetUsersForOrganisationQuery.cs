using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Queries.GetUsersForOrganisationQuery
{
    public record GetUsersForOrganisationQuery():IRequest<IEnumerable<UserDto>>
    {
    }
}
