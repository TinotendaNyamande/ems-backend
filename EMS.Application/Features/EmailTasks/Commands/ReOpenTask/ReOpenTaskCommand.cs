using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.ReOpenTask
{
    public record ReOpenTaskCommand(Guid TaskId) : IRequest
    {
    }
}