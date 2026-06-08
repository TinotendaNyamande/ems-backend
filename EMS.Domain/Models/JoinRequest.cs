namespace EMS.Domain.Models
{
    public class JoinRequest
    {
        public Guid Id { get; private set; }
        public string RequestById { get;private set; }
        public Guid OrganisationId { get; private set; }
        public Organisation? Organisation { get;private set; }
        public DateTime RequestedAt { get; private set; } 
        public bool IsApproved { get; private set; } 
        public DateTime? ApprovedAt { get; private set; }
        public string? ApprovedByUserId { get; private set; }
        public bool IsRejected { get; private set; }
        public string? RejectedByUserId { get; private set; }
        public DateTime? RejectedAt { get; private set; }
        public JoinRequest(string requestById, Guid organisationId)
        {
            Id = Guid.NewGuid();
            RequestById = requestById;
            OrganisationId = organisationId;
            RequestedAt = DateTime.Now;
            IsApproved = false;
            IsApproved= false;
        }


        public void Approve(string approverUserId)
        {
            IsApproved = true;
            ApprovedAt = DateTime.UtcNow;
            ApprovedByUserId = approverUserId;
        }
        public void Reject(string rejectorUserId)
        {
            IsRejected = true;
            RejectedByUserId = rejectorUserId;
            RejectedAt=DateTime.UtcNow;    
        }
    }
}
