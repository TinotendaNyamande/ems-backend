namespace EMS.Application.Dtos.RolesAndPermissions
{
    public class EditPermissionDto

    {
        public Guid PermissionId { get; set; } 
        public Guid OrganisationRoleId { get; set; }
        public string PermissionKey { get; set; }
        public bool IsAllowed { get; set; }
    }
}
