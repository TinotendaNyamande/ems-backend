using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Application.Features.EmailAccounts.Queries.GetAllValidatedEmailAccounts;
using EMS.Application.Features.EmailCategoryUserMatrix.Commands.RecordUserAssignedTaskAction;
using EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetAllUsersAvailableForCategory;
using EMS.Application.Features.Emails.Commands.MarkEmailAsAssigned;
using EMS.Application.Features.Emails.Queries.GetEmailsPendingAssignment;
using EMS.Application.Features.EmailTasks.Commands.CreateTask;
using EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry;
using EMS.Domain.Models;
using MediatR;

namespace EMS.TaskAssignment.Worker.Services
{
    public class TaskAssignmentService(IMediator mediator, ILogger<TaskAssignmentService> logger
    ) : ITaskAssignmentService
    {
        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            var emailAccounts = await mediator.Send(new GetAllValidatedEmailAccountsQuery(), cancellationToken);

            foreach (var emailAccount in emailAccounts)
            {
                var unassignedEmails = await mediator.Send(new GetEmailsPendingAssignmentQuery(emailAccount.Id), cancellationToken);
                foreach (var email in unassignedEmails)
                {
                    logger.LogInformation("Found unassigned email {subject}", email.Subject);
                    logger.LogInformation("Email category {id}", email.EmailCategoryId);
                    if (email.EmailCategoryId != Guid.Empty && email.EmailCategoryId != null)
                    {
                        var matrix = await mediator.Send(new GetAllUsersAvailableForCategoryQuery(email.EmailCategoryId), cancellationToken);//await matrixRepository.GetAllAvailableForCategoryAsync(email.EmailCategoryId);
                        logger.LogInformation("Found {count} categories matrix", matrix.Count());
                        var pickedMatrix = await DetermineUserToAssign(matrix);
                        logger.LogInformation("Picked user for task {id}", pickedMatrix.UserId);
                        var task = await mediator.Send(new CreateTaskCommand(email.Id, pickedMatrix.UserId), cancellationToken);

                        await mediator.Send(new MarkEmailAsAssignedCommand(email.Id), cancellationToken);

                        await mediator.Send(new RecordUserAssignedTaskActionCommand(pickedMatrix.Id), cancellationToken);

                        await mediator.Send(new CreateSLAEntryCommand(task.Id, "New task assigned"), cancellationToken);


                        await mediator.Send(new CreateAuditTrailEntryCommand(task.Id, "System", "Task assigned to user"),cancellationToken);
                    }

                }
            }


        }
        private static async Task<EmailCategoriesUserMatrix> DetermineUserToAssign(IEnumerable<EmailCategoriesUserMatrix> matrix)
        {
            var candidates = matrix.Where(x => x.IsAvailable).ToList();

            if (candidates.Count == 0)
                throw new InvalidOperationException("No available users found for this category.");

            var selected = candidates
                .OrderBy(x => x.LastAssignedAt == default ? DateTime.MinValue : x.LastAssignedAt)
                .ThenBy(x => x.Id)
                .First();

            return selected;
        }
    }
}