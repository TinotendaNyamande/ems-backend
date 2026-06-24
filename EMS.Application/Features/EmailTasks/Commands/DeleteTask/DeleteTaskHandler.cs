using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.DeleteTask
{
    internal class DeleteTaskHandler(IEmailTasksRepository tasksRepository):IRequestHandler<DeleteTaskCommand>
    {
        public async Task Handle(DeleteTaskCommand command,CancellationToken token)
        {
            await tasksRepository.DeleteTaskAsync(command.Id);
        }
    }
}