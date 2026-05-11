using AutoMapper;
using EMS.Application.Features.UsersManagement.Commands.AssignUserRole;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class RolesAndPermissionsProfile:Profile
    {
        public RolesAndPermissionsProfile()
        {
            CreateMap<OrganisationUserRole, AssignUserRoleCommand>();
        }
    }
}
