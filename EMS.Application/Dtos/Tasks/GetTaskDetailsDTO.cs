using EMS.Domain.Enums;

namespace EMS.Application.Dtos.Tasks
{
    public record GetTasksDetailsDto(Guid Id, string FromEmail, string Subject, string EmailBody, string EmailAccountAddress, string AssignedToUserId, string AssignedToUserFirstName, string AssignedToUserLastName, DateTime CreatedAt, DateTime UpdatedAt, DateTime? AssignedToUserDate, DateTime? ClosedDate, TaskStatusList Status, string? AdditionalInformation,string Category,List<GetAttachmentDTO> Attachments);
}