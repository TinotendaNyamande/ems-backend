using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.DeleteTask
{
    public record DeleteTaskCommand (Guid Id) : IRequest
    {
        
    }
}