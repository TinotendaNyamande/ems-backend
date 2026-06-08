namespace EMS.Domain.Models
{
    public class OrganisationRolePermission
    {
        public Guid Id { get; private set; } 
        public OrganisationRole? OrganisationRole { get; private set; }
        public Guid OrganisationRoleId  { get;private  set; }
        public string PermissionKey { get; private set; }
        public bool IsAllowed { get;private set; }
        public OrganisationRolePermission(Guid organisationRoleId,string permissionKey,bool isAllowed)
        {
            Id = Guid.NewGuid();
            OrganisationRoleId = organisationRoleId;
            PermissionKey = permissionKey;
            IsAllowed = isAllowed;
        }

        public void ChangePermission(bool isAllowed)
        {
            IsAllowed = isAllowed;
        }
    }
}
