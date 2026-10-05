using EMS.Application.Abstractions;

namespace EMS.Application.Features.EmailTasks.Commands.ReassignTask
{
    public record ReassignTaskCommand (Guid TaskId,string NewUserId,string UserId) : ICommand
    {
        
    }
}