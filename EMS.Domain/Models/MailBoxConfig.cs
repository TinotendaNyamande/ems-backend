using EMS.Domain.Enums;

namespace EMS.Domain.Models
{
    public class MailBoxConfig
    {
        public Guid Id { get; private set; }
        public string EmailAddress { get; private set; }
        public EmailType EmailType { get; private set; }
        public string? Password { get; private set; }
        public string? ClientId { get; private set; }
        public string? ClientSecret { get; private set; }
        public string? TenantId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }
        public ICollection<EmailInbox> EmailInboxes{ get; private set; }
        public Organisation Organisation { get; private set; }
        public Guid OrganisationId { get; private set; }

        public MailBoxConfig(EmailType emailType, string emailAddress, string? password, string? clientId, string? clientSecret, string? tenantId,Guid organisationId)
        {
            if(organisationId==Guid.Empty) throw new ArgumentException("Vaue cannot be empty",nameof(organisationId));
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentException("Value cannot be empty", nameof(emailAddress));

            if (!emailAddress.Contains("@"))
                throw new ArgumentException("Invalid email format", nameof(emailAddress));

            EmailType = emailType;
            EmailAddress = emailAddress;
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
            Id = Guid.NewGuid();
            Password = null;
            ClientId = null;
            ClientSecret = null;
            TenantId = null;
            EmailInboxes= [];
            OrganisationId=organisationId;

            switch (emailType)
            {
                case EmailType.Gmail:
                case EmailType.Outlook:
                case EmailType.Custom:
                    if (string.IsNullOrWhiteSpace(password))
                        throw new ArgumentException("Password is required for this email type", nameof(password));

                    Password = password;
                    break;

                case EmailType.Office365:
                    if (string.IsNullOrWhiteSpace(clientId))
                        throw new ArgumentException("Value cannot be empty", nameof(clientId));

                    if (string.IsNullOrWhiteSpace(clientSecret))
                        throw new ArgumentException("Value cannot be empty", nameof(clientSecret));

                    if (string.IsNullOrWhiteSpace(tenantId))
                        throw new ArgumentException("Value cannot be empty", nameof(tenantId));

                    ClientId = clientId;
                    ClientSecret = clientSecret;
                    TenantId = tenantId;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(emailType), "Unsupported email type");
            }
        }
        public void ChangePassword(string password)
        {
            if (EmailType == EmailType.Office365)
                throw new InvalidOperationException("Password is not used for Office365");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty", nameof(password));
            
            Password = password;
            LastUpdatedAt = DateTime.UtcNow;
        }
        public void ChangeClientSecret(string clientSecret)
        {
            if (EmailType != EmailType.Office365)
                throw new InvalidOperationException("Client secret is only valid for Office365");

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException("Value cannot be empty", nameof(clientSecret));
            
            ClientSecret = clientSecret;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
