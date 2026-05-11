namespace EMS.Application.Dtos.RolesAndPermissions
{
    public class GetPermissionDto
    {
        public Guid Id { get; set; }
        public Guid OrganisationRoleId { get; set; }
        public string PermissionKey { get; set; }
        public bool IsAllowed { get; set; }
    }
}
