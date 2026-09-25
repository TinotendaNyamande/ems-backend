using EMS.Application.Abstractions;
using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Application.Features.EmailTasks.Queries.GetTaskById;
using EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry;
using EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry;
using EMS.Application.Features.SLAEntriesTracking.Queries.GetRunningSLAEntryForTask;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    internal class UpdateStatusHandler(IEmailTasksRepository tasksRepository, IMediator mediator, ILogger<UpdateStatusHandler> logger
    ) : ICommandHandler<UpdateStatusCommand>
    {
        public async Task Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating status for task {TaskId} to {NewStatus}", command.Id, command.NewStatus);
            await tasksRepository.ChangeTaskStatusAsync(
                command.Id,
                command.NewStatus,
                command.AdditionalInformation);
            var runningSLAEntry = await mediator.Send(new GetRunningSLAEntryForTaskQuery(command.Id), cancellationToken);
            var task = await mediator.Send(new GetTaskByIdQuery(command.Id),cancellationToken);
            await mediator.Send(new CreateAuditTrailEntryCommand(command.Id, command.UserId, $"Task status changed to {command.NewStatus}"), cancellationToken);
            if (command.NewStatus == TaskStatusList.Closed)
            {

                {
                    await mediator.Send(new UpdateSLAEntryCommand(runningSLAEntry.Id, DateTime.UtcNow, SLAEntryStatus.Stopped), cancellationToken);
                }

            }
            if (command.NewStatus == TaskStatusList.Hold)
            {
                {
                    if (runningSLAEntry != null)
                    {
                        await mediator.Send(new UpdateSLAEntryCommand(runningSLAEntry.Id, DateTime.UtcNow, SLAEntryStatus.Stopped), cancellationToken);
                    }
                    await mediator.Send(new CreateSLAEntryCommand(command.Id, task.AssignedToUser, "Task put on hold"), cancellationToken);


                }
            }
            if (command.NewStatus == TaskStatusList.Assigned)
            {

                if (runningSLAEntry != null)
                {
                    await mediator.Send(new UpdateSLAEntryCommand(runningSLAEntry.Id, DateTime.UtcNow, SLAEntryStatus.Stopped), cancellationToken);

                }
                await mediator.Send(new CreateSLAEntryCommand(command.Id, task.AssignedToUser, "Task assigned to user"), cancellationToken);


            }
            if (command.NewStatus == TaskStatusList.Escalated)
            {

                if (runningSLAEntry != null)
                {
                    await mediator.Send(new UpdateSLAEntryCommand(runningSLAEntry.Id, DateTime.UtcNow, SLAEntryStatus.Stopped), cancellationToken);

                }
                await mediator.Send(new CreateSLAEntryCommand(command.Id, task.AssignedToUser, "Task escalated"), cancellationToken);


            }

        }
    }
}