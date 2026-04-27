namespace EMS.Domain.Models
{
    public class Organisation
    {
        public Guid Id { get; private set; } 
        public string Name { get; private set; }
        public string OwnerId { get; private set; } 
        public DateTime CreatedAt { get; private set; }

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
            this.OwnerId = ownerId;
            this.Name = name;
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.UtcNow;
        }
        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Organisation name cannot be empty" , nameof(newName));
            }
            this.Name = newName;
        }
        public void ChangeOwner(string newOwnerId)
        {
            if (string.IsNullOrWhiteSpace(newOwnerId))
            {
                throw new ArgumentException("OwnerId cannot be empty", nameof(newOwnerId));
            }
            this.OwnerId = newOwnerId;
        }
    }
}
