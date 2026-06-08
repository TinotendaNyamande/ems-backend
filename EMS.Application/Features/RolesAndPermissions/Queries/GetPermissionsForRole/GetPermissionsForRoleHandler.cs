using AutoMapper;
using EMS.Application.Dtos.RolesAndPermissions;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.RolesAndPermissions.Queries.GetPermissionsForRole
{
    internal class GetPermissionsForRoleHandler(IRolePermissionsRepository rolePermissionsRepository,IMapper mapper) : IRequestHandler<GetPermissionsForRoleQuery, IEnumerable<GetPermissionDto>>
    {
        public async Task<IEnumerable<GetPermissionDto>> Handle(GetPermissionsForRoleQuery request, CancellationToken cancellationToken)
        {
            var permissions =await  rolePermissionsRepository.GetPermissionsForRole(request.RoleId);
            return mapper.Map<IEnumerable<GetPermissionDto>>(permissions);
        }
    }
}
