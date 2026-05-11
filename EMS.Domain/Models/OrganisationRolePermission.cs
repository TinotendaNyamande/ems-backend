namespace EMS.Domain.Models
{
    public class OrganisationRolePermission
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public OrganisationRole OrganisationRole { get; set; }
        public Guid OrganisationRoleId  { get;set; }
        public string PermissionKey { get; set; }
        public bool IsAllowed { get;set; }

        public void ChangePermission(bool isAllowed)
        {
            IsAllowed = isAllowed;
        }
    }
}
