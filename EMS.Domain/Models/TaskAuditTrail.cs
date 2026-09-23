namespace EMS.Domain.Models
{
    public class TaskAuditTrail(string comments,string userId,Guid emailTaskId)
    {
        public Guid Id {get;private set;}= Guid.NewGuid();
        public string Comments {get;private set;}=comments;
        public DateTime CreatedAt {get;private set;}= DateTime.UtcNow;
        public string UserId {get;private set;}= userId;
        public EmailTask EmailTask {get;private set;}
        public Guid EmailTaskId {get;private set;}=emailTaskId;
    }
}