using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    internal class UpdateStatusHandler(IEmailTasksRepository tasksRepository, ISLATrackingRepository sLATrackingRepository,
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
                        await sLATrackingRepository.UpdateAsync(slaEntry.Id, updateSLAEntryDto);
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
                            Comments = "Task put on hold"
                        };
                        await sLATrackingRepository.UpdateAsync(slaEntry.Id, updateSLAEntryDto);
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
                            Comments = "Task resumed from hold"
                        };
                        await sLATrackingRepository.UpdateAsync(slaEntry.Id, updateSLAEntryDto);
                        var newSLAEntry = new SLATracking(task.Id,task.AssignedToUser, "Task resumed from hold");
                        await sLATrackingRepository.AddAsync(newSLAEntry);
                    }
                }
            }

        }
    }
}