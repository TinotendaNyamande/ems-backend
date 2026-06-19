using EMS.Domain.Enums;

namespace EMS.Application.Dtos.EmailAccounts
{
    public class ValidationAndTestEmailAccountDto
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public EmailType EmailType { get; set; }
        public string? Password { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? TenantId { get; set; }
        public Guid OrganisationId { get; set; }
        public bool IsValidated { get; set; }

    }
}