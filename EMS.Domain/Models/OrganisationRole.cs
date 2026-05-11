namespace EMS.Domain.Models
{
    public class OrganisationRole
    {
        public Guid Id { get;private set; } = Guid.NewGuid();
        public Guid OrganisationId { get;set; }
        public Organisation Organisation   { get;  set; }
        public string RoleName { get;  set; }
        public ICollection<OrganisationRolePermission> Permissions { get; set; }
    }
}
