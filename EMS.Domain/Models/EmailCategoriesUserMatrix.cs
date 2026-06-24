namespace EMS.Domain.Models
{
    public class EmailCategoriesUserMatrix
    {
        public Guid Id {get;private set;}= Guid.NewGuid();
        public EmailCategory? EmailCategory {get;private set;}
        public Guid EmailCategoryId {get;private set;}
        public string? UserId {get;private set;}
        public bool IsAvailable {get;private set;}=true;
        public DateTime? LastAssignedAt {get;private set;}

        public void ChangeLastAssignedDate()
        {
            LastAssignedAt=DateTime.UtcNow;
        }
    

    }
}