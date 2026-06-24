using Microsoft.AspNetCore.Identity;

namespace EMS.Infrastructure.persistence
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName {get;set;}
        public string LastName { get;set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
        public Guid? OrganisationId { get;private set; }
        public DateTime LastLoginDate { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public Guid EmailCategoryId {get;private set;}

        public void AddOrganisationIdToUser(Guid organisationId)
        {
            OrganisationId = organisationId;
        }
        public void UpdateLastLoginDate()
        {
            LastLoginDate = DateTime.UtcNow;
        }
        public void ChangeEmailCategory (Guid newCategoryId)
        {
            EmailCategoryId=newCategoryId;
        }



    }
}
