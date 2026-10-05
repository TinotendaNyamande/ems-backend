using EMS.Application.Abstractions;
using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry;
using EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry;
using EMS.Application.Features.SLAEntriesTracking.Queries.GetRunningSLAEntryForTask;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Application.Features.EmailTasks.Commands.ReassignTask
{
    internal class ReassignTaskHandler(
        IEmailTasksRepository tasksRepository,
         IUserService userService,
          ITaskAuditRepository auditRepository,
          ISLATrackingRepository slaTrackingRepository
          ) : ICommandHandler<ReassignTaskCommand>
    {
        public async Task Handle(ReassignTaskCommand command, CancellationToken cancellationToken)
        {
            await userService.GetUserByIdAsync(command.NewUserId);

            await auditRepository.CreateTaskAuditTrailAsync(new TaskAuditTrail("Task Reassigned", command.UserId, command.TaskId));
            await tasksRepository.ReassignTaskAsync(command.TaskId, command.NewUserId);
            var currentSLAEntry = await slaTrackingRepository.GetCurrentEntryForTaskAsync(command.TaskId);
            if (currentSLAEntry != null)
            {
                await slaTrackingRepository.StopTimerAsync(currentSLAEntry.Id);

            }
            await slaTrackingRepository.CreateSLAEntryAsync(new SLATracking(command.TaskId, command.NewUserId, "Task reassigned"));

        }
    }
}