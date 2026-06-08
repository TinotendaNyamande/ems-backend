using AutoMapper;
using EMS.Application.Dtos.RolesAndPermissions;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.RolesAndPermissions.Queries.GetPermissionById
{
    public class GetPermissionByIdHandler(IRolePermissionsRepository permissionRepository, IMapper mapper) : IRequestHandler<GetPermissionByIdQuery, GetPermissionDto>
    {
        public async Task<GetPermissionDto> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            var permmission = await permissionRepository.GetPermissionAsync(request.PermissionId);
            return mapper.Map<GetPermissionDto>(permmission);
        }
    }
}
