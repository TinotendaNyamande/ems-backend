namespace EMS.Domain.Models
{
    public class Organisation(string ownerId, string name)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = name;
        public string OwnerId { get; private set; } = ownerId;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public ICollection<MailBoxConfig> MailBoxes { get; private set; } = [];
        public ICollection<EmailCategory> EmailCategories { get; private set; } = [];

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Organisation name cannot be empty" , nameof(newName));
            }
            Name = newName;
        }
        public void ChangeOwner(string newOwnerId)
        {
            if (string.IsNullOrWhiteSpace(newOwnerId))
            {
                throw new ArgumentException("OwnerId cannot be empty", nameof(newOwnerId));
            }
            OwnerId = newOwnerId;
        }
    }
}
