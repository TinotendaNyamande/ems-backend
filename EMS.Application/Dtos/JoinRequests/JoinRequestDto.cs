using EMS.Application.Dtos.Auth;

namespace EMS.Application.Dtos.JoinRequests
{
    public class JoinRequestDto
    {
        public Guid Id { get; set; }
        public UserDto RequestedBy { get; set; }
        public DateTime RequestedAt { get; set; }
        public  string Status { get; set; }
        public UserDto? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public UserDto? RejectedBy { get; set; }
        public DateTime? RejectedAt { get; set; }
    }
}
