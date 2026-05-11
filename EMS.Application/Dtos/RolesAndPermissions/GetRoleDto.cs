namespace EMS.Application.Dtos.RolesAndPermissions
{
    public class GetRoleDto
    {
        public Guid Id { get; set; } 
        public string RoleName { get; set; }
        public ICollection<GetPermissionDto> Permissions { get; set; }
    }
}
