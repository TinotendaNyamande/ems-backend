namespace EMS.Domain.Models
{
    public class EmailCategory(Guid emailAccountId, string categoryName,double SLAInHours)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid EmailAccountId { get; private set; } = emailAccountId;
        public string CategoryName { get; private set; } = categoryName;
        public double SLAInHours { get; private set; } = SLAInHours;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; private set; } = DateTime.UtcNow;
        public ICollection<Email> Emails { get; private set; } = [];

        public void  ChangeCategoryName (string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Value cannot be empty", nameof(categoryName));
            
            CategoryName = categoryName;
            LastUpdatedAt = DateTime.UtcNow;
        }
        public void ChangeSLAInHours(double sLAInHours)
        {
            if (sLAInHours <= 0)
                throw new ArgumentException("Value must be greater than zero", nameof(sLAInHours));
            
            SLAInHours = sLAInHours;
            LastUpdatedAt = DateTime.UtcNow;
        }


    }
}
