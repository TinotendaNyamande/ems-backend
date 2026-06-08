using EMS.Application.Dtos.RolesAndPermissions;
using MediatR;

namespace EMS.Application.Features.RolesAndPermissions.Queries.GetPermissionsForRole
{
    public record GetPermissionsForRoleQuery(Guid RoleId) : IRequest<IEnumerable<GetPermissionDto>>
    {
    }
}
