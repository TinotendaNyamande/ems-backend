using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace EMS.Infrastructure.persistence
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName {get;set;}
        public string LastName { get;set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
        public Organisation?Organisation { get; set; }
        public Guid? OrganisationId { get;private set; }

        public void AddOrganisationIdToUser(Guid organisationId)
        {
            OrganisationId = organisationId;
        }



    }
}
