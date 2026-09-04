using EMS.Domain.Enums;

namespace EMS.Domain.Models
{
    public class EmailTask(Guid emailId, string assignedToUser)
    {
        public Guid Id { get; private set; } = new Guid();
        public Email? Email { get; private set; }
        public Guid EmailId { get; private set; } = emailId;
        public string? AssignedToUser { get; private set; } = assignedToUser;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime AssignedToUserDate { get; private set; } = DateTime.UtcNow;
        public DateTime ClosedDate { get; private set; }
        public TaskStatusList Status { get; set; } = TaskStatusList.Assigned;
        public string? AdditionalInformation { get; private set; }
        public ICollection<SLATracking> SLATrackings { get; private set; } = new List<SLATracking>();

        public void AssignToUser(string newUserId)
        {
            AssignedToUser = newUserId;
            AssignedToUserDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public void CloseTask(string additionalInfo)
        {
            AdditionalInformation = additionalInfo;
            Status = TaskStatusList.Closed;
            ClosedDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

        }
        public void ChangeStatus(TaskStatusList newStatus)
        {
            if (newStatus == TaskStatusList.Closed)
            {
                ClosedDate = DateTime.UtcNow;
            }
            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }
        public void EditAdditionalInfo(string info)
        {
            AdditionalInformation = info;
            UpdatedAt = DateTime.UtcNow;
        }
        public void ReOpenTask()
        {
            Status = TaskStatusList.Assigned;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}