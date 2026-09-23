using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailTaskRepository(ApplicationDbContext context, ILogger<EmailTaskRepository> logger) : IEmailTasksRepository
    {

        public async Task ChangeTaskStatusAsync(Guid id, TaskStatusList newStatus, string? additionalInformation = null)
        {
            var task = await context.EmailTasks.Where(t => t.Id == id).FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("Task", id);
            task.ChangeStatus(newStatus);
            if (!string.IsNullOrEmpty(additionalInformation))
            {
                task.EditAdditionalInfo(additionalInformation);
            }
            await context.SaveChangesAsync();
        }

        public async Task CloseTaskAsync(Guid id, string additionalInformation)
        {
            var task = await context.EmailTasks.Where(t => t.Id == id).FirstOrDefaultAsync() ??
             throw new ResourceNotFoundException("Task", id);
            task.CloseTask(additionalInformation);
            await context.SaveChangesAsync();
        }

        public async Task<EmailTask> CreateTaskAsync(EmailTask emailTask)
        {
            context.Add(emailTask);
            await context.SaveChangesAsync();
            return emailTask;
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var affectedRows = await context.EmailTasks.Where(t => t.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                throw new ResourceNotFoundException("Task", id);
            }
        }

        public async Task EditAdditionalInformationAsync(Guid id, string additionalInfo)
        {
            var task = await context.EmailTasks.Where(t => t.Id == id).FirstOrDefaultAsync() ??
             throw new ResourceNotFoundException("Task", id);
            task.EditAdditionalInfo(additionalInfo);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetTasksDto>> GetAllOpenTasksAsync()
        {
             var query =
            from tasks in context.EmailTasks
            join user in context.Users
            on tasks.AssignedToUser equals user.Id
            join emails in context.Emails
            on tasks.EmailId equals emails.Id
            join emailsAccounts in context.EmailAccounts
            on emails.EmailAccountId equals emailsAccounts.Id
            where tasks.Status != TaskStatusList.Closed
            join category in context.EmailCategories
            on emails.EmailCategoryId equals category.Id
            select new GetTasksDto
            {
                Id = tasks.Id,
                FromEmail = emails.FromEmail,
                Subject = emails.Subject,
                EmailBody = emails.Body,
                EmailAccountAddress = emailsAccounts.EmailAddress,
                AssignedToUser = tasks.AssignedToUser,
                AssignedToUserFirstName = user.FirstName,
                AssignedToUserLastName = user.LastName,
                CreatedAt = tasks.CreatedAt,
                UpdatedAt = tasks.UpdatedAt,
                AssignedToUserDate = tasks.AssignedToUserDate,
                ClosedDate = tasks.ClosedDate,
                Status = tasks.Status,
                AdditionalInformation = tasks.AdditionalInformation,
                Category = category.CategoryName

            };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<GetTasksDto>> GetAllOpenTasksByUserIdAsync(string userId)
        {
             var query =
            from tasks in context.EmailTasks
            join user in context.Users
            on tasks.AssignedToUser equals user.Id
            join emails in context.Emails
            on tasks.EmailId equals emails.Id
            join emailsAccounts in context.EmailAccounts
            on emails.EmailAccountId equals emailsAccounts.Id
            where tasks.AssignedToUser == userId & tasks.Status != TaskStatusList.Closed
            join category in context.EmailCategories
            on emails.EmailCategoryId equals category.Id
            select new GetTasksDto
            {
                Id = tasks.Id,
                FromEmail = emails.FromEmail,
                Subject = emails.Subject,
                EmailBody = emails.Body,
                EmailAccountAddress = emailsAccounts.EmailAddress,
                AssignedToUser = tasks.AssignedToUser,
                AssignedToUserFirstName = user.FirstName,
                AssignedToUserLastName = user.LastName,
                CreatedAt = tasks.CreatedAt,
                UpdatedAt = tasks.UpdatedAt,
                AssignedToUserDate = tasks.AssignedToUserDate,
                ClosedDate = tasks.ClosedDate,
                Status = tasks.Status,
                AdditionalInformation = tasks.AdditionalInformation,
                Category = category.CategoryName

            };

            return await query.ToListAsync();
        }

        public  async Task<IEnumerable<GetTasksDto>> GetAllTasksAsync()
        {
             var query =
            from tasks in context.EmailTasks
            join user in context.Users
            on tasks.AssignedToUser equals user.Id
            join emails in context.Emails
            on tasks.EmailId equals emails.Id
            join emailsAccounts in context.EmailAccounts
            on emails.EmailAccountId equals emailsAccounts.Id
            join category in context.EmailCategories
            on emails.EmailCategoryId equals category.Id
            select new GetTasksDto
            {
                Id = tasks.Id,
                FromEmail = emails.FromEmail,
                Subject = emails.Subject,
                EmailBody = emails.Body,
                EmailAccountAddress = emailsAccounts.EmailAddress,
                AssignedToUser = tasks.AssignedToUser,
                AssignedToUserFirstName = user.FirstName,
                AssignedToUserLastName = user.LastName,
                CreatedAt = tasks.CreatedAt,
                UpdatedAt = tasks.UpdatedAt,
                AssignedToUserDate = tasks.AssignedToUserDate,
                ClosedDate = tasks.ClosedDate,
                Status = tasks.Status,
                AdditionalInformation = tasks.AdditionalInformation,
                Category = category.CategoryName

            };

            return await query.ToListAsync();
        }

        public async Task<GetTasksDto> GetTaskByIdAsync(Guid id)
        {
            var query =
                from tasks in context.EmailTasks
                join user in context.Users
                on tasks.AssignedToUser equals user.Id
                join emails in context.Emails
                on tasks.EmailId equals emails.Id
                join emailsAccounts in context.EmailAccounts
                on emails.EmailAccountId equals emailsAccounts.Id
                join category in context.EmailCategories
                on emails.EmailCategoryId equals category.Id
                where tasks.Id == id
                select new GetTasksDto
                {
                    Id = tasks.Id,
                    FromEmail = emails.FromEmail,
                    Subject = emails.Subject,
                    EmailBody = emails.Body,
                    EmailAccountAddress = emailsAccounts.EmailAddress,
                    AssignedToUser = tasks.AssignedToUser,
                    AssignedToUserFirstName = user.FirstName,
                    AssignedToUserLastName = user.LastName,
                    CreatedAt = tasks.CreatedAt,
                    UpdatedAt = tasks.UpdatedAt,
                    AssignedToUserDate = tasks.AssignedToUserDate,
                    ClosedDate = tasks.ClosedDate,
                    Status = tasks.Status,
                    AdditionalInformation = tasks.AdditionalInformation,
                    Category = category.CategoryName

                };
            return await query.FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Task", id);
        }

        public async Task<IEnumerable<GetTasksDto>> GetTasksByUserIdAsync(string userId, TaskStatusList? status)
        {

            var query =
            from tasks in context.EmailTasks
            join user in context.Users
            on tasks.AssignedToUser equals user.Id
            join emails in context.Emails
            on tasks.EmailId equals emails.Id
            join emailsAccounts in context.EmailAccounts
            on emails.EmailAccountId equals emailsAccounts.Id
            where tasks.AssignedToUser == userId
            join category in context.EmailCategories
            on emails.EmailCategoryId equals category.Id
            select new GetTasksDto
            {
                Id = tasks.Id,
                FromEmail = emails.FromEmail,
                Subject = emails.Subject,
                EmailBody = emails.Body,
                EmailAccountAddress = emailsAccounts.EmailAddress,
                AssignedToUser = tasks.AssignedToUser,
                AssignedToUserFirstName = user.FirstName,
                AssignedToUserLastName = user.LastName,
                CreatedAt = tasks.CreatedAt,
                UpdatedAt = tasks.UpdatedAt,
                AssignedToUserDate = tasks.AssignedToUserDate,
                ClosedDate = tasks.ClosedDate,
                Status = tasks.Status,
                AdditionalInformation = tasks.AdditionalInformation,
                Category = category.CategoryName

            };
            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }
            return await query.ToListAsync();


        }

        public async Task<IEnumerable<GetTasksDto>> GetTasksForAccountAsync(Guid emailAccountId, TaskStatusList? status)
        {

            var query =
                from tasks in context.EmailTasks
                join user in context.Users
                on tasks.AssignedToUser equals user.Id
                join emails in context.Emails
                on tasks.EmailId equals emails.Id
                join emailsAccounts in context.EmailAccounts
                on emails.EmailAccountId equals emailsAccounts.Id
                select new GetTasksDto
                {
                    Id = tasks.Id,
                    FromEmail = emails.FromEmail,
                    Subject = emails.Subject,
                    EmailBody = emails.Body,
                    EmailAccountAddress = emailsAccounts.EmailAddress,
                    AssignedToUser = tasks.AssignedToUser,
                    AssignedToUserFirstName = user.FirstName,
                    AssignedToUserLastName = user.LastName,
                    CreatedAt = tasks.CreatedAt,
                    UpdatedAt = tasks.UpdatedAt,
                    AssignedToUserDate = tasks.AssignedToUserDate,
                    ClosedDate = tasks.ClosedDate,
                    Status = tasks.Status,
                    AdditionalInformation = tasks.AdditionalInformation,
                    Category = ""//category.CategoryName

                };
            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);

            }
            return await query.ToListAsync();



        }

        public async Task<UserTasksSummaryDto> GetUserTasksSummaryAsync(string userId)
        {
            var tasksTotal = await context.EmailTasks.Where(a=>a.AssignedToUser==userId).ToListAsync();
            var openTasks = await context.EmailTasks.Where(a=>a.AssignedToUser==userId && a.Status== TaskStatusList.Assigned).ToListAsync();
            var holdTasks = await context.EmailTasks.Where(a=>a.AssignedToUser==userId && a.Status== TaskStatusList.Hold).ToListAsync();
            var closedTasks = await context.EmailTasks.Where(a=>a.AssignedToUser==userId && a.Status== TaskStatusList.Closed).ToListAsync();
            var slaEntries = await context.SLATrackings.Where(a=>a.UserId==userId).ToListAsync();
            var averageResolutionTime = slaEntries.Any() ? slaEntries.Average(s => s.DurationInHours) : 0;
            return new UserTasksSummaryDto(tasksTotal.Count,holdTasks.Count,openTasks.Count,closedTasks.Count,averageResolutionTime);
        }

        public async Task ReassignTaskAsync(Guid id, string newUserId)
        {
            logger.LogInformation("Reassigning task {TaskId} to user {NewUserId}", id, newUserId);
            var task = await context.EmailTasks.Where(t => t.Id == id).FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("Task", id);
            logger.LogInformation("Task {TaskId} found. Current assigned user: {CurrentUserId}", id, task.AssignedToUser);
            task.AssignToUser(newUserId);
            logger.LogInformation("Task {TaskId} reassigned to user {NewUserId}. Updating database.", id, newUserId);
            await context.SaveChangesAsync();
            logger.LogInformation("Task {TaskId} successfully reassigned to user {NewUserId}.", id, newUserId);
        }
        public async Task ReOpenTaskAsync(Guid id)
        {
            var task = await context.EmailTasks.Where(t => t.Id == id).FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("Task", id);
            task.ReOpenTask();
            await context.SaveChangesAsync();
        }
    }
}