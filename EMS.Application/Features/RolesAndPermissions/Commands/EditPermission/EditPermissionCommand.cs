using MediatR;

namespace EMS.Application.Features.RolesAndPermissions.Commands.EditPermission
{
    public record EditPermissionCommand(Guid PermissionId, Guid OrganisationRoleId, string PermissionKey, bool IsAllowed) : IRequest
    {

    }
}
