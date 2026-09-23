namespace EMS.Application.Dtos.TaskAuditTrail
{
    public record CreateTaskAuditTrailDto(string UserId,string Comment,Guid EmailTaskId){};
}