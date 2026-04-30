namespace EMS.Domain.Models
{
    public class Organisation
    {
        public Guid Id { get; private set; } 
        public string Name { get; private set; }
        public string OwnerId { get; private set; } 
        public DateTime CreatedAt { get; private set; }
        public ICollection<MailBoxConfig> MailBoxes { get; private set; }
        public ICollection<EmailCategory> EmailCategories { get; private set; }

        public Organisation(string ownerId,string name)
        {
            if (string.IsNullOrWhiteSpace(ownerId))
            {
                throw new ArgumentException("OwnerId cannot be empty", nameof(ownerId));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Organisation name cannot be empty", nameof(name));
            }
            OwnerId = ownerId;
            Name = name;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            MailBoxes = [];
            EmailCategories = [];
        }
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
