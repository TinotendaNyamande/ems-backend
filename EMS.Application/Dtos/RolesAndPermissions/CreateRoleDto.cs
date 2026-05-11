namespace EMS.Application.Dtos.RolesAndPermissions
{
    public class CreateRoleDto
    {
        public Guid OrganisationId { get; set; }
        public string RoleName { get; set; }
    }
}
