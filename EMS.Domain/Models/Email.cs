using EMS.Domain.Enums;

namespace EMS.Domain.Models
{
    public class Email(string fromEmail, string toEmail, string subject, string body, Guid emailAccountId,string externalMessageId)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FromEmail { get; private set; } = fromEmail;
        public string ToEmail { get; private set; } = toEmail;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        public string? Subject { get; private set; } = subject;
        public string? Body { get; private set; } = body;
        public EmailStatus Status { get; private set; } = EmailStatus.New;
        public EmailAccount EmailAccount { get; private set; }
        public Guid EmailAccountId { get; private set; } = emailAccountId;
        public EmailCategory? EmailCategory { get; private set; }
        public Guid? EmailCategoryId { get; private set; }
        public ICollection<EmailAttachment> EmailAttachments { get; private set; } = new List<EmailAttachment>();
        public int Order { get; private set; } = 1;
        public string? AssignedTo { get; private set; }
        public string? ExternalMessageId{get;private set;}=externalMessageId;

        public void ChangeStatus(EmailStatus status)
        {

            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }
        public void ChangeCategory(Guid emailCategoryId)
        {
            if (emailCategoryId == Guid.Empty) throw new ArgumentException("Value cannot be empty", nameof(emailCategoryId));
            EmailCategoryId = emailCategoryId;
            UpdatedAt = DateTime.UtcNow;
        }
        public void ChangeOrder(int order)
        {
            if (order < 1) throw new ArgumentException("Invalid argument", nameof(order));
            Order = order;
            UpdatedAt = DateTime.UtcNow;
        }
        public void AddAttachment(EmailAttachment attachment)
        {
            ArgumentNullException.ThrowIfNull(attachment);
            EmailAttachments.Add(attachment);
        }
        public void AssignToUser(string userId)
        {
            AssignedTo = userId;
        }

    }
}
