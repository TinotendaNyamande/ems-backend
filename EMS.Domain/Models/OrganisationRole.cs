using EMS.Domain.Enums;

namespace EMS.Domain.Models
{
    public class OrganisationRole
    {
        public Guid Id { get;private set; } 
        public Guid OrganisationId { get;private set; }
        public Organisation? Organisation   { get;  private set; }
        public string RoleName { get;  private set; }
        public ICollection<OrganisationRolePermission> Permissions { get; private set; }
        private OrganisationRole()
        {
            Permissions = new List<OrganisationRolePermission>();
        }

        public OrganisationRole(Guid organisationId,string roleName,IEnumerable<string> permissions)
        {
            var allowedSet = permissions.ToHashSet(StringComparer.OrdinalIgnoreCase);
            Id = Guid.NewGuid();
            OrganisationId = organisationId;
            RoleName = roleName;
            Permissions = PermissionCatalog.All
            .Select(permission => new OrganisationRolePermission(Id,permission.Key,allowedSet.Contains(permission.Key)))
            .ToList();
        }
    }
}
