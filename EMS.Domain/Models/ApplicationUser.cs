using Microsoft.AspNetCore.Identity;

namespace ems.domain.models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
    }
}
