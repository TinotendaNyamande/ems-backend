using EMS.Domain.Enums;

namespace EMS.Application.Dtos.Tasks
{
    public class GetTasksDto
    {
        public Guid Id { get; set; } 
        public string? FromEmail { get;set; } 
        public string? Subject {get;set;}
        public string? EmailBody {get;set;}
        public string? EmailAccountAddress {get;set;}
        public string? AssignedToUser { get;set; }
        public string? AssignedToUserFirstName { get;set; }
        public string? AssignedToUserLastName { get;set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; } 
        public DateTime? AssignedToUserDate { get;set; }
        public DateTime? ClosedDate { get; set; }
        public TaskStatusList Status { get; set; } 
        public string? AdditionalInformation { get; set; }
        public string? Category {get;set;}
    }
}