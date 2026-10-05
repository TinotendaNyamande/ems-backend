using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.Emails.Commands.AssignEmailToUser
{
    internal class AssignEmailToUserHandler(
        IEmailTasksRepository emailTasksRepository,
        IEmailRepository emailRepository,
         ILogger<AssignEmailToUserHandler> logger,
          IEmailCategoriesUserMatrixRepository matrixRepository,
          ITaskAssignmentService taskAssignmentService,
          ISLATrackingRepository slaTrackingRepository,
          ITaskAuditRepository taskAuditRepository
          ) : ICommandHandler<AssignEmailToUserCommand>
    {
        public async Task Handle(AssignEmailToUserCommand request, CancellationToken cancellationToken)
        {

            var matrix = await matrixRepository.GetAllAvailableForCategoryAsync(request.EmailCategoryId);
            logger.LogInformation("Found {count} categories matrix", matrix.Count());

            var pickedUserMatrix = await taskAssignmentService.PickUserAsync(matrix);
            logger.LogInformation("Picked user for task {id}", pickedUserMatrix.UserId);

            var task = await emailTasksRepository.CreateTaskAsync(new EmailTask(request.EmailId, pickedUserMatrix.UserId));
            await emailRepository.MarkEmailAsAssignedAsync(request.EmailId);
            await matrixRepository.RecordUserAssignedTaskActionAsync(pickedUserMatrix.Id);
            await slaTrackingRepository.CreateSLAEntryAsync(new SLATracking(task.Id, pickedUserMatrix.UserId, "New task assigned"));
            await taskAuditRepository.CreateTaskAuditTrailAsync(new TaskAuditTrail("New task assigned", pickedUserMatrix.UserId, task.Id));

        }
    }
}