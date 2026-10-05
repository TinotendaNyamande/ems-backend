using EMS.Application.Abstractions;

namespace EMS.Application.Features.EmailTasks.Commands.DeleteTask
{
    public record DeleteTaskCommand (Guid Id,string UserId) : ICommand
    {
        
    }
}