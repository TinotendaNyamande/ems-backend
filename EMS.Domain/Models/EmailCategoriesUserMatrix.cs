namespace EMS.Domain.Models
{
    public class EmailCategoriesUserMatrix(string userId, Guid emailCategoryId)
    {
        public Guid Id {get;private set;}= Guid.NewGuid();
        public EmailCategory EmailCategory {get;private set;}=null!;
        public Guid EmailCategoryId {get;private set;} = emailCategoryId;
        public string UserId {get;private set;} = userId;
        public bool IsAvailable {get;private set;}=true;
        public DateTime? LastAssignedAt {get;private set;}=null;

        public void ChangeLastAssignedDate()
        {
            LastAssignedAt=DateTime.UtcNow;
        }
    

    }
}