namespace EMS.Domain.Models
{
    public class OrganisationUserRole(string userId, Guid roleId)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string UserId { get; private set; } = userId;
        public OrganisationRole Role { get; private set; } = null!;
        public Guid RoleId { get; private set; } = roleId;
    }
}
