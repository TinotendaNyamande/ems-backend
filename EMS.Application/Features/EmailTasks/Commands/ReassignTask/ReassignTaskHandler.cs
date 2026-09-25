using EMS.Application.Abstractions;
using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry;
using EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry;
using EMS.Application.Features.SLAEntriesTracking.Queries.GetRunningSLAEntryForTask;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.ReassignTask
{
    internal class ReassignTaskHandler(IMediator mediator, IEmailTasksRepository tasksRepository, IUserService userService, ISLATrackingRepository sLATrackingRepository, ITaskAuditRepository taskAuditRepository) : ICommandHandler<ReassignTaskCommand>
    {
        public async Task Handle(ReassignTaskCommand command, CancellationToken cancellationToken)
        {
            await userService.GetUserByIdAsync(command.NewUserId);

            await mediator.Send(new CreateAuditTrailEntryCommand(command.TaskId, command.UserId, "Task Reassigned"), cancellationToken);
            await tasksRepository.ReassignTaskAsync(command.TaskId, command.NewUserId);
            var currentSLAEntry = await mediator.Send(new GetRunningSLAEntryForTaskQuery(command.TaskId), cancellationToken);
            if (currentSLAEntry != null)
            {
                await mediator.Send(new UpdateSLAEntryCommand(currentSLAEntry.Id, DateTime.UtcNow, SLAEntryStatus.Stopped), cancellationToken);

            }
            await mediator.Send(new CreateSLAEntryCommand(command.TaskId, command.UserId, "Task reassigned"), cancellationToken);

        }
    }
}