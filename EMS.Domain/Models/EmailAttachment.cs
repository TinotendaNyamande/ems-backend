namespace EMS.Domain.Models
{
    public class EmailAttachment(Guid emailInboxId, string fileName, string filePath, string fileType)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public EmailInbox EmailInbox { get; private set; }
        public Guid EmailInboxId { get; private set; } = emailInboxId;
        public string FileName { get; private set; } = fileName;
        public string FilePath { get; private set; } = filePath;
        public string FileType { get; private set; } = fileType;
    }
}
