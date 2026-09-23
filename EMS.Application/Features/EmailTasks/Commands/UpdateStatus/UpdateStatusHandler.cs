using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    internal class UpdateStatusHandler(IEmailTasksRepository tasksRepository, ISLATrackingRepository sLATrackingRepository,
    ITaskAuditRepository taskAuditRepository, ILogger<UpdateStatusHandler> logger
    ) : IRequestHandler<UpdateStatusCommand>
    {
        public async Task Handle(UpdateStatusCommand command, CancellationToken token)
        {
            logger.LogInformation("Updating status for task {TaskId} to {NewStatus}", command.Id, command.NewStatus);

            await tasksRepository.ChangeTaskStatusAsync(
                command.Id,
                command.NewStatus,
                command.AdditionalInformation);
            var audit = new TaskAuditTrail($"Task status changed to {command.NewStatus}", command.UserId, command.Id);
            await taskAuditRepository.CreateTaskAuditTrailAsync(audit);
            if (command.NewStatus == TaskStatusList.Closed)
            {
                var task = await tasksRepository.GetTaskByIdAsync(command.Id);
                if (task != null)
                {
                    var slaEntry = await sLATrackingRepository.GetCurrentEntryForTaskAsync(task.Id);
                    if (slaEntry != null)
                    {
                        var updateSLAEntryDto = new UpdateSLAEntryDto
                        {
                            EndTime = DateTime.UtcNow,
                            Status = SLAEntryStatus.Stopped,
                            Comments = command.AdditionalInformation
                        };
                        await sLATrackingRepository.StopTimerAsync(slaEntry.Id, updateSLAEntryDto);
                    }

                }
            }
            if (command.NewStatus == TaskStatusList.Hold)
            {
                var task = await tasksRepository.GetTaskByIdAsync(command.Id);
                if (task != null)
                {
                    var slaEntry = await sLATrackingRepository.GetCurrentEntryForTaskAsync(task.Id);
                    if (slaEntry != null)
                    {
                        var updateSLAEntryDto = new UpdateSLAEntryDto
                        {
                            EndTime = DateTime.UtcNow,
                            Status = SLAEntryStatus.Stopped,
                        };
                        await sLATrackingRepository.StopTimerAsync(slaEntry.Id, updateSLAEntryDto);
                        var newSLAEntry = new SLATracking(task.Id, task.AssignedToUser, "Task put on hold");
                        await sLATrackingRepository.AddAsync(newSLAEntry);
                    }

                }
            }
            if (command.NewStatus == TaskStatusList.Assigned)
            {
                var task = await tasksRepository.GetTaskByIdAsync(command.Id);
                if (task != null)
                {
                    var slaEntry = await sLATrackingRepository.GetCurrentEntryForTaskAsync(task.Id);
                    if (slaEntry != null)
                    {
                        var updateSLAEntryDto = new UpdateSLAEntryDto
                        {
                            EndTime = DateTime.UtcNow,
                            Status = SLAEntryStatus.Stopped,
                        };
                        await sLATrackingRepository.StopTimerAsync(slaEntry.Id, updateSLAEntryDto);
                        var newSLAEntry = new SLATracking(task.Id, task.AssignedToUser, "Task resumed from hold");
                        await sLATrackingRepository.AddAsync(newSLAEntry);
                    }
                }
            }
            if (command.NewStatus == TaskStatusList.Escalated)
            {
                var task = await tasksRepository.GetTaskByIdAsync(command.Id);
                if (task != null)
                {
                    var slaEntry = await sLATrackingRepository.GetCurrentEntryForTaskAsync(task.Id);
                    if (slaEntry != null)
                    {
                        var updateSLAEntryDto = new UpdateSLAEntryDto
                        {
                            EndTime = DateTime.UtcNow,
                            Status = SLAEntryStatus.Stopped,
                        };
                        await sLATrackingRepository.StopTimerAsync(slaEntry.Id, updateSLAEntryDto);
                        var newSLAEntry = new SLATracking(task.Id, task.AssignedToUser, "Task Escalated");
                        await sLATrackingRepository.AddAsync(newSLAEntry);
                    }
                }
            }

        }
    }
}