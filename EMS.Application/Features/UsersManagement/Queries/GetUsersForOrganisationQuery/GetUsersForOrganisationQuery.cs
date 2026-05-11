using EMS.Application.Dtos.Auth;
using MediatR;

namespace EMS.Application.Features.UsersManagement.Queries.GetUsersForOrganisationQuery
{
    public record GetUsersForOrganisationQuery(Guid OrganisationId):IRequest<IEnumerable<UserDto>>
    {
    }
}
