using EMS.Application.Dtos.Organisation;
using EMS.Domain.Enums;

namespace EMS.Application.Dtos.EmailConfigs
{
    public class EmailConfigDto
    {
        public Guid Id { get; private set; }
        public string EmailAddress { get; private set; }
        public EmailType EmailType { get; private set; }
        public string? Password { get; private set; }
        public string? ClientId { get; private set; }
        public string? ClientSecret { get; private set; }
        public string? TenantId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrganisationDto ParentOrganisation { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }
        public bool IsValidated { get; private set; }
    }
}
