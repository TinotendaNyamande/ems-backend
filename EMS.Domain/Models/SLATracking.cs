using EMS.Domain.Enums;

namespace EMS.Domain.Models
{
    public class SLATracking(Guid emailTaskId, string userId,string comments)
    {
        public Guid Id { get;private set; }= Guid.NewGuid();
        public EmailTask EmailTask { get;private set; }
        public Guid EmailTaskId { get;private set; }= emailTaskId;
        public DateTime StartTime { get;private set; }= DateTime.UtcNow;
        public DateTime? EndTime { get; private set; }
        public SLAEntryStatus Status { get; private set; }= SLAEntryStatus.Running;
        public string UserId { get; private set; }= userId;
        public string Comments { get; private set; }= comments;
        public double? DurationInHours { get; private set; }
        public void StopEntry(string comments)
        {
            EndTime = DateTime.UtcNow;
            Status = SLAEntryStatus.Stopped;
            Comments = comments;
            DurationInHours = (EndTime - StartTime)?.TotalHours;
        }

    }
}