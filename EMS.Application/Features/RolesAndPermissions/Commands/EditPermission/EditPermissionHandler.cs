using AutoMapper;
using EMS.Application.Dtos.RolesAndPermissions;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.RolesAndPermissions.Commands.EditPermission
{
    internal class EditPermissionHandler(IRolePermissionsRepository rolePermissionsRepository,IMapper mapper) : IRequestHandler<EditPermissionCommand>
    {
        public async Task Handle(EditPermissionCommand request, CancellationToken cancellationToken)
        {
            var permissions = mapper.Map<EditPermissionDto>(request);
            await rolePermissionsRepository.EditPermissionsAsync(request.PermissionId, permissions);
        }
    }
}
