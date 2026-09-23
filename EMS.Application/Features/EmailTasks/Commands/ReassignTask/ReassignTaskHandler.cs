using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.ReassignTask
{
    internal class ReassignTaskHandler(IEmailTasksRepository tasksRepository, IUserService userService, ISLATrackingRepository sLATrackingRepository,ITaskAuditRepository taskAuditRepository) : IRequestHandler<ReassignTaskCommand>
    {
        public async Task Handle(ReassignTaskCommand command, CancellationToken token)
        {
            await userService.GetUserByIdAsync(command.NewUserId);
            var audit = new TaskAuditTrail("Task Reassigned", command.UserId, command.Id);
            await taskAuditRepository.CreateTaskAuditTrailAsync(audit);
            await tasksRepository.ReassignTaskAsync(command.Id, command.NewUserId);
            var currentSLAEntry = await sLATrackingRepository.GetCurrentEntryForTaskAsync(command.Id);
            if (currentSLAEntry != null)
            {
                var entryDto = new UpdateSLAEntryDto
                {
                    Comments = currentSLAEntry.Comments,
                    EndTime = DateTime.UtcNow,
                    Status = SLAEntryStatus.Stopped
                };
                await sLATrackingRepository.StopTimerAsync(currentSLAEntry.Id, entryDto);
            }
            var newSLAEntry = new SLATracking(command.Id, command.NewUserId, "Task reassigned");
            await sLATrackingRepository.AddAsync(newSLAEntry);
        }
    }
}