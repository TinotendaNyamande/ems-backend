namespace EMS.Domain.Models
{
    public class EmailAttachment
    {
        public Guid Id { get; private set; }
        public EmailInbox EmailInbox { get; private set; }
        public Guid EmailInboxId { get; private set; }
        public string FileName { get; private set; }
        public string FilePath { get;private set; }
        public string FileType { get;private set; }
        public EmailAttachment(Guid emailInboxId,string fileName, string filePath,string fileType)
        {
            if(emailInboxId==Guid.Empty)
                throw new ArgumentException("Value cannot be empty",nameof(emailInboxId));
            if(string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Value cannot be empty" , nameof(fileName));
            if(string.IsNullOrWhiteSpace(fileType))
                throw new ArgumentException("Value cannot be null" , nameof (fileType));
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Value cannot be null", nameof(filePath));

            Id = Guid.NewGuid();
            FileName = fileName;
            FilePath = filePath;
            FileType = fileType;
            EmailInboxId = emailInboxId;
        }
    }
}
