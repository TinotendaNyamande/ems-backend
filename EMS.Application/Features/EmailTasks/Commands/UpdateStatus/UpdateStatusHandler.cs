using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    internal class UpdateStatusHandler(IEmailTasksRepository tasksRepository,
    ILogger<UpdateStatusHandler> logger
    ) : IRequestHandler<UpdateStatusCommand>
    {
        public async Task Handle(UpdateStatusCommand command, CancellationToken token)
        {
            logger.LogInformation("Updating status for task {TaskId} to {NewStatus}", command.Id, command.NewStatus);

            await tasksRepository.ChangeTaskStatusAsync(
                command.Id,     
                command.NewStatus,
                command.AdditionalInformation);
        }
    }
}