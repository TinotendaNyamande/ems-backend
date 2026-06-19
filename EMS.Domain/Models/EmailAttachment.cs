namespace EMS.Domain.Models
{
    public class EmailAttachment(Guid emailId, string fileName, string filePath, string fileType)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Email Email { get; private set; }
        public Guid EmailId { get; private set; } = emailId;
        public string FileName { get; private set; } = fileName;
        public string FilePath { get; private set; } = filePath;
        public string FileType { get; private set; } = fileType;
    }
}
