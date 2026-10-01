using EMS.Domain.Enums;

namespace EMS.Application.Dtos.Tasks
{
    public class GetTasksDto
    {
        public Guid Id { get; set; } 
        public string? FromEmail { get;set; } 
        public string? Subject {get;set;}
        public string? AssignedToUserId { get;set; }
        public string? AssignedToUserFirstName { get;set; }
        public string? AssignedToUserLastName { get;set; }
        public TaskStatusList Status { get; set; } 
        public string? Category {get;set;}
        public DateTime CreatedAt { get; set; }
    }
}