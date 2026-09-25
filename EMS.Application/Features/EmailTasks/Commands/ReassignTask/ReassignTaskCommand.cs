using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.ReassignTask
{
    public record ReassignTaskCommand (Guid TaskId,string NewUserId,string UserId) : ICommand
    {
        
    }
}