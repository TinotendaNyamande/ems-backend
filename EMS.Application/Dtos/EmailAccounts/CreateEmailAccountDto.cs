using EMS.Domain.Enums;

namespace EMS.Application.Dtos.EmailAccounts
{
    public class CreateEmailAccountDto
    {
        
        public string EmailAddress { get; set; }
        public EmailType EmailType { get; set; }
        public string? Password { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; private set; }
        public string? TenantId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }
        public Guid OrganisationId { get; private set; }
        public bool IsValidated { get; private set; } 

    }
}