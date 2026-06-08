using EMS.Application.Dtos.RolesAndPermissions;
using MediatR;

namespace EMS.Application.Features.RolesAndPermissions.Queries.GetPermissionById
{
    public record GetPermissionByIdQuery(Guid PermissionId):IRequest<GetPermissionDto>
    {
    }
}
