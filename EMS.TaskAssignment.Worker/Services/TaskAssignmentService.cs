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
                       
                    }

                }
            }


        }

    }
}