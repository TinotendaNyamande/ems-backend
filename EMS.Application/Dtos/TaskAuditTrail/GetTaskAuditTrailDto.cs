namespace EMS.Application.Dtos.TaskAuditTrail
{
    public record GetTaskAuditTrailDto(Guid Id,string UserName,string Comment,DateTime CreatedAt,Guid EmailTaskId){};
}