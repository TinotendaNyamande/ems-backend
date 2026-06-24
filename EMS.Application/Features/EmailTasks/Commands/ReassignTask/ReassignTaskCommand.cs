using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.ReassignTask
{
    public record ReassignTaskCommand (Guid Id,string NewUserId) : IRequest
    {
        
    }
}