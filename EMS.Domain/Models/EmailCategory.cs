namespace EMS.Domain.Models
{
    public class EmailCategory(Guid organisationId, string categoryName)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Organisation Organisation { get; private set; }
        public Guid OrganisationId { get; private set; } = organisationId;
        public string CategoryName { get; private set; } = categoryName;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; private set; } = DateTime.UtcNow;
        public ICollection<EmailInbox> EmailInboxes { get; private set; } = [];

        public void  ChangeCategoryName (string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Value cannot be empty", nameof(categoryName));
            
            this.CategoryName = categoryName;
            this.LastUpdatedAt = DateTime.UtcNow;
        }


    }
}
