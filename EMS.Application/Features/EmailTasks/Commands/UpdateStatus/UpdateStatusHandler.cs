using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    internal class UpdateStatusHandler(IEmailTasksRepository tasksRepository) : IRequestHandler<UpdateStatusCommand>
    {
        public async Task Handle(UpdateStatusCommand command, CancellationToken token)
        {
          await tasksRepository.ChangeTaskStatusAsync(command.Id,command.NewStatus);
        }
    }
}