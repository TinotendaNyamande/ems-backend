using EMS.Application.Abstractions;
using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Application.Features.EmailTasks.Queries.GetTaskById;
using EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry;
using EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry;
using EMS.Application.Features.SLAEntriesTracking.Queries.GetRunningSLAEntryForTask;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using Microsoft.Extensions.Logging;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    internal class UpdateStatusHandler(
        IEmailTasksRepository tasksRepository,
        ISLATrackingRepository slaTrackingRepository,
        ITaskAuditRepository taskAuditRepository,
         ILogger<UpdateStatusHandler> logger
    ) : ICommandHandler<UpdateStatusCommand>
    {
        public async Task Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating status for task {TaskId} to {NewStatus}", command.Id, command.NewStatus);
            await tasksRepository.ChangeTaskStatusAsync(
                command.Id,
                command.NewStatus,
                command.AdditionalInformation);
            var runningSLAEntry = await slaTrackingRepository.GetCurrentEntryForTaskAsync(command.Id);
            var task = await tasksRepository.GetTaskByIdAsync(command.Id)
            ??throw new ResourceNotFoundException("EmailTask", command.Id);
            await taskAuditRepository.CreateTaskAuditTrailAsync(new TaskAuditTrail($"Task status changed to {command.NewStatus}", command.UserId, command.Id));
            if (command.NewStatus == TaskStatusList.Closed && runningSLAEntry != null)
            {

                {
                    await slaTrackingRepository.StopTimerAsync(runningSLAEntry.Id);
                }

            }
            if (command.NewStatus == TaskStatusList.Hold)
            {
                {
                    if (runningSLAEntry != null)
                    {
                        await slaTrackingRepository.StopTimerAsync(runningSLAEntry.Id);
                    }
                    await slaTrackingRepository.CreateSLAEntryAsync(new SLATracking(command.Id, task.AssignedToUserId, "Task put on hold"));


                }
            }
            if (command.NewStatus == TaskStatusList.Assigned)
            {

                if (runningSLAEntry != null)
                {
                    await slaTrackingRepository.StopTimerAsync(runningSLAEntry.Id);
                }
                await slaTrackingRepository.CreateSLAEntryAsync(new SLATracking(command.Id, task.AssignedToUserId, "Task assigned to user"));

            }
            if (command.NewStatus == TaskStatusList.Escalated)
            {

                if (runningSLAEntry != null)
                {
                    await slaTrackingRepository.StopTimerAsync(runningSLAEntry.Id);
                }
                await slaTrackingRepository.CreateSLAEntryAsync(new SLATracking(command.Id, task.AssignedToUserId, "Task escalated"));


            }

        }
    }
}