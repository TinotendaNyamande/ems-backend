namespace EMS.Domain.Models
{
    public class EmailCategory
    {
        public Guid Id { get; private set; }
        public Organisation Organisation { get; private set; }
        public Guid OrganisationId { get; private set; }
        public string CategoryName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }
        public ICollection<EmailInbox> EmailInboxes { get; private set; }

        public EmailCategory(Guid organisationId,string categoryName)
        {
            if(string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Value cannot be empty" ,nameof(categoryName));
            if(organisationId == Guid.Empty)
                throw new ArgumentException("Value cannot be empty",nameof(organisationId));

            OrganisationId = organisationId;
            CategoryName = categoryName;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
            EmailInboxes = [];
        }
        public void  ChangeCategoryName (string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Value cannot be empty", nameof(categoryName));
            
            this.CategoryName = categoryName;
            this.LastUpdatedAt = DateTime.UtcNow;
        }


    }
}
