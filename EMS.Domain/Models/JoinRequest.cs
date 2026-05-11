namespace EMS.Domain.Models
{
    public class JoinRequest
    {
        public Guid Id { get; private set; }=Guid.NewGuid();
        public string RequestById { get;private set; }
        public Guid OrganisationId { get; private set; }
        public Organisation Organisation { get;private set; }
        public DateTime RequestedAt { get; private set; } = DateTime.Now;
        public bool IsApproved { get; private set; } = false;
        public DateTime? ApprovedAt { get; private set; }
        public string? ApprovedByUserId { get; private set; }
        public string? RejectionReason { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public bool IsRejected { get; private set; } = false;
        public string? RejectedByUserId { get; private set; }
        public DateTime RejectedAt { get; private set; }

        public void Approve(string approverUserId)
        {
            IsApproved = true;
            ApprovedAt = DateTime.UtcNow;
            ApprovedByUserId = approverUserId;
        }
        public void Reject(string reason,string rejectorUserId)
        {
            IsRejected = true;
            RejectionReason = reason;
            RejectedByUserId = rejectorUserId;
            RejectedAt=DateTime.UtcNow;    
        }
    }
}
