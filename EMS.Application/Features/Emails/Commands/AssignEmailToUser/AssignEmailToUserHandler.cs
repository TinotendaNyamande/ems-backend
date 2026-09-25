using EMS.Application.Abstractions;
using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Application.Features.EmailCategoryUserMatrix.Commands.RecordUserAssignedTaskAction;
using EMS.Application.Features.Emails.Commands.MarkEmailAsAssigned;
using EMS.Application.Features.EmailTasks.Commands.CreateTask;
using EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry;
using EMS.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.Emails.Commands.AssignEmailToUser
{
    internal class AssignEmailToUserHandler(ILogger<AssignEmailToUserHandler> logger, IEmailCategoriesUserMatrixRepository matrixRepository, ITaskAssignmentService taskAssignmentService, IMediator mediator) : ICommandHandler<AssignEmailToUserCommand>
    {
        public async Task Handle(AssignEmailToUserCommand request, CancellationToken cancellationToken)
        {

            var matrix = await matrixRepository.GetAllAvailableForCategoryAsync(request.EmailCategoryId);
            logger.LogInformation("Found {count} categories matrix", matrix.Count());

            var pickedUserMatrix = await taskAssignmentService.PickUserAsync(matrix);
            logger.LogInformation("Picked user for task {id}", pickedUserMatrix.UserId);

            var task = await mediator.Send(new CreateTaskCommand(request.EmailId, pickedUserMatrix.UserId), cancellationToken);

            await mediator.Send(new MarkEmailAsAssignedCommand(request.EmailId), cancellationToken);
            await mediator.Send(new RecordUserAssignedTaskActionCommand(pickedUserMatrix.Id), cancellationToken);
            await mediator.Send(new CreateSLAEntryCommand(task.Id,pickedUserMatrix.UserId, "New task assigned"), cancellationToken);
            await mediator.Send(new CreateAuditTrailEntryCommand(task.Id, "System", "Task assigned to user"), cancellationToken);

        }
    }
}