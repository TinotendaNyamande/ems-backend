using EMS.Application.Dtos.RolesAndPermissions;
using MediatR;

namespace EMS.Application.Features.RolesAndPermissions.Queries.GetRolesForOrganisation
{
    public record GetRolesForOrganisationQuery(Guid OrganisationId) : IRequest<IEnumerable<GetRoleDto>>
    {
    }
}
