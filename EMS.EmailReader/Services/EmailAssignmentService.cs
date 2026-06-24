using EMS.Application.Interfaces;
using EMS.Domain.Models;

namespace EMS.EmailReader.Services
{
    public class EmailAssignmentService
    (
IEmailRepository emailRepository,
IEmailAccountRepository emailAccountRepository,
IEmailCategoriesUserMatrixRepository matrixRepository,
IEmailTasksRepository emailTasksRepository,
ILogger<EmailAssignmentService> logger
    ) : IEmailAssignmentService
    {
        public async Task AssignEmailsAsync()
        {
            var emailAccounts = await emailAccountRepository.GetAllValidatedAsync();
        
            foreach (var emailAccount in emailAccounts)
            {
                var unassignedEmails = await emailRepository.GetEmailsPendingAssignment(emailAccount.Id);
                foreach (var email in unassignedEmails)
                {
                    logger.LogInformation("Found unassigned email {subject}",email.Subject);
                    logger.LogInformation("Email category {id}",email.EmailCategoryId);
                    if (email.EmailCategoryId != Guid.Empty && email.EmailCategoryId != null)
                    {
                        var matrix = await matrixRepository.GetAllAvailableForCategory(email.EmailCategoryId);
                        logger.LogInformation("Found {count} categories matrix",matrix.Count());
                        var pickedMatrix = await DetermineUserToAssign(matrix);
                        logger.LogInformation("Picked user for task {id}",pickedMatrix.UserId);
                        var task = new EmailTask(email.Id, pickedMatrix.UserId);
                        await emailTasksRepository.CreateTaskAsync(task);
                        await emailRepository.EmailAssignedAction(email.Id);
                        await matrixRepository.UserAssignedTaskAction(pickedMatrix.Id);
                    }

                }
            }


        }
        private async Task<EmailCategoriesUserMatrix> DetermineUserToAssign(IEnumerable<EmailCategoriesUserMatrix> matrix)
        {
            var candidates = matrix.Where(x => x.IsAvailable).ToList();

            if (!candidates.Any())
                throw new InvalidOperationException("No available users found for this category.");

            var selected = candidates
                .OrderBy(x => x.LastAssignedAt == default ? DateTime.MinValue : x.LastAssignedAt)
                .ThenBy(x => x.Id)
                .First();

            return selected;
        }
    }
}