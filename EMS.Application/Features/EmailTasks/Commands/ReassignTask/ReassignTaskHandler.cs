using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.ReassignTask
{
    internal class ReassignTaskHandler(IEmailTasksRepository tasksRepository) : IRequestHandler<ReassignTaskCommand>
    {
        public async Task Handle(ReassignTaskCommand command, CancellationToken token)
        {
            await tasksRepository.ReassignTaskAsync(command.Id, command.NewUserId);
        }
    }
}