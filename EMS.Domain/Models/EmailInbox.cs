using EMS.Domain.Enums;

namespace EMS.Domain.Models
{
    public class EmailInbox
    {
        public  Guid Id { get; private set; }
        public string FromEmail { get; private set; }
        public string ToEmail { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public EmailStatus Status { get; private set; }
        public MailBoxConfig? MailBoxConfig { get; private set; }
        public Guid MailBoxConfigId { get; private set; }
        public EmailCategory? EmailCategory { get; private set; }
        public Guid? EmailCategoryId { get; private set; }
        public ICollection<EmailAttachment> EmailAttachments { get; private set; }
        public int Order { get; private set; } = 1;

        public EmailInbox(string fromEmail,string toEmail,string subject, string body,Guid mailBoxConfigId)
        {
            if(mailBoxConfigId == Guid.Empty) throw new ArgumentException("Value cannot be empty",nameof(mailBoxConfigId));
            if (string.IsNullOrWhiteSpace(fromEmail) || string.IsNullOrWhiteSpace(toEmail) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
                throw new ArgumentException("Invalid email");

            Id = Guid.NewGuid();
            FromEmail = fromEmail;
            ToEmail = toEmail;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Subject = subject;
            Body = body;
            Status = EmailStatus.New;
            MailBoxConfigId = mailBoxConfigId;
            EmailAttachments = new List<EmailAttachment>();
        }
        public void ChangeStatus(EmailStatus status) 
        { 

            Status = status; 
            UpdatedAt= DateTime.UtcNow;
        }
        public void ChangeCategory(Guid emailCategoryId)
        {
            if (emailCategoryId ==Guid.Empty) throw new ArgumentException("Value cannot be empty", nameof(emailCategoryId));
            EmailCategoryId= emailCategoryId;
            UpdatedAt= DateTime.UtcNow;
        }
        public void ChangeOrder(int order)
        {
            if(order < 1) throw new ArgumentException("Invalid argument" ,nameof(order));
            Order = order;
            UpdatedAt= DateTime.UtcNow;
        }
        public void AddAttachment(EmailAttachment attachment)
        {
            ArgumentNullException.ThrowIfNull(attachment);
            EmailAttachments.Add(attachment);
        }

    }
}
