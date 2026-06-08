using AutoMapper;
using EMS.Application.Dtos.RolesAndPermissions;
using EMS.Application.Features.RolesAndPermissions.Commands.EditPermission;
using EMS.Application.Features.UsersManagement.Commands.AssignUserRole;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class RolesAndPermissionsProfile:Profile
    {
        public RolesAndPermissionsProfile()
        {
            CreateMap<AssignUserRoleCommand,OrganisationUserRole>();
           
            CreateMap<OrganisationRole, GetRoleDto>()
                .ForMember(dest => dest.Permissions,
                opt => opt.MapFrom(src => src.Permissions.Select(p => new GetPermissionDto { 
                    Id= p.Id,
                    IsAllowed=p.IsAllowed,
                    PermissionKey = p.PermissionKey 
                })));
            CreateMap<EditPermissionCommand, EditPermissionDto>();
        }
    }
}
