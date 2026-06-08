using AutoMapper;
using EMS.Application.Dtos.RolesAndPermissions;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class RolesAndPermissionsMappingProfile:Profile
    {
        public RolesAndPermissionsMappingProfile()
        {
            CreateMap<OrganisationRolePermission, GetPermissionDto>();
        }
    }
}
