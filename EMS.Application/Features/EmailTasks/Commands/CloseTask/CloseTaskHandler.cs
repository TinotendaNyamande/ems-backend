using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.CloseTask
{
    internal class CloseTaskHandler(IEmailTasksRepository tasksRepository) : IRequestHandler<CloseTaskCommand>
    {
        public async Task Handle(CloseTaskCommand command,CancellationToken token)
        {
            await tasksRepository.CloseTaskAsync(command.Id,command.AdditionalInfo);
        }
    }
}