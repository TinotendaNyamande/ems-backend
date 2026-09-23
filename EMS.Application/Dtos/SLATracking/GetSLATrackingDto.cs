using EMS.Domain.Enums;

namespace EMS.Application.Dtos.SLATracking
{
    public class GetSLATrackingDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Comments { get; set; }
        public SLAEntryStatus Status { get; set; }
        public string? UserName {get;set;}
    }
}