using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EMS.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailTaskRepository(ApplicationDbContext context, ILogger<EmailTaskRepository> logger) : IEmailTasksRepository
    {

        public async Task ChangeTaskStatusAsync(Guid id, TaskStatusList newStatus, string? additionalInformation = null)
        {
            var task = await context.EmailTasks.FindAsync(id) ??
            throw new ResourceNotFoundException("Task", id);
            task.ChangeStatus(newStatus);
            if (!string.IsNullOrEmpty(additionalInformation))
            {
                task.EditAdditionalInfo(additionalInformation);
            }
        }

        public async Task CloseTaskAsync(Guid id, string additionalInformation)
        {
            var task = await context.EmailTasks.FindAsync(id) ??
             throw new ResourceNotFoundException("Task", id);
            task.CloseTask(additionalInformation);
        }

        public async Task<EmailTask> CreateTaskAsync(EmailTask emailTask)
        {
            context.Add(emailTask);
            return emailTask;
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var task = await context.EmailTasks.FindAsync(id) ??
             throw new ResourceNotFoundException("Task", id);
            context.Remove(task);
        }

        public async Task EditAdditionalInformationAsync(Guid id, string additionalInfo)
        {
            var task = await context.EmailTasks.FindAsync(id) ??
             throw new ResourceNotFoundException("Task", id);
            task.EditAdditionalInfo(additionalInfo);
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
               AssignedToUserId = tasks.AssignedToUser,
               AssignedToUserFirstName = user.FirstName,
               AssignedToUserLastName = user.LastName,
               Status = tasks.Status,
               Category = category.CategoryName,
               CreatedAt = tasks.CreatedAt

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
           where tasks.AssignedToUser == userId && tasks.Status != TaskStatusList.Closed
           join category in context.EmailCategories
           on emails.EmailCategoryId equals category.Id
           select new GetTasksDto
           {
               Id = tasks.Id,
               FromEmail = emails.FromEmail,
               Subject = emails.Subject,
               AssignedToUserId = tasks.AssignedToUser,
               AssignedToUserFirstName = user.FirstName,
               AssignedToUserLastName = user.LastName,
               Status = tasks.Status,
               Category = category.CategoryName,
               CreatedAt = tasks.CreatedAt
           };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<GetTasksDto>> GetAllTasksAsync()
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
               AssignedToUserId = tasks.AssignedToUser,
               AssignedToUserFirstName = user.FirstName,
               AssignedToUserLastName = user.LastName,
               Status = tasks.Status,
               Category = category.CategoryName,
               CreatedAt = tasks.CreatedAt
           };

            return await query.ToListAsync();
        }

        public async Task<GetTasksDetailsDto?> GetTaskByIdAsync(Guid id)
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
                select new GetTasksDetailsDto(tasks.Id, emails.FromEmail, emails.Subject!, emails.Body!, emailsAccounts.EmailAddress, tasks.AssignedToUser, user.FirstName!, user.LastName!, tasks.CreatedAt, tasks.UpdatedAt, tasks.AssignedToUserDate, tasks.ClosedDate, tasks.Status, tasks.AdditionalInformation, category.CategoryName,
                emails.EmailAttachments.Select(a => new GetAttachmentDTO(a.Id, a.FileName, a.FileType, a.FileSize)).ToList());


                
            return await query.FirstOrDefaultAsync();
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
                AssignedToUserId = tasks.AssignedToUser,
                AssignedToUserFirstName = user.FirstName,
                AssignedToUserLastName = user.LastName,
                Status = tasks.Status,
                Category = category.CategoryName,
                CreatedAt = tasks.CreatedAt

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
                where emails.EmailAccountId == emailAccountId
                join category in context.EmailCategories
                on emails.EmailCategoryId equals category.Id
                select new GetTasksDto
                {
                    Id = tasks.Id,
                    FromEmail = emails.FromEmail,
                    Subject = emails.Subject,
                    AssignedToUserId = tasks.AssignedToUser,
                    AssignedToUserFirstName = user.FirstName,
                    AssignedToUserLastName = user.LastName,
                    Status = tasks.Status,
                    Category = category.CategoryName,
                    CreatedAt = tasks.CreatedAt
                };
            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);

            }
            return await query.ToListAsync();



        }

        public async Task<UserTasksSummaryDto> GetUserTasksSummaryAsync(string userId)
        {
            var counts = await context.EmailTasks
                .Where(t => t.AssignedToUser == userId)
                .GroupBy(t => t.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            var averageResolutionTime = await context.SLATrackings
                .Where(s => s.UserId == userId)
                .Select(s => s.DurationInHours)
                .AverageAsync() ?? 0;
            var tasksTotal = counts.Where(c => c.Status == TaskStatusList.Assigned || c.Status == TaskStatusList.Hold || c.Status == TaskStatusList.Closed).ToList();
            var holdTasks = counts.Where(c => c.Status == TaskStatusList.Hold).ToList();
            var openTasks = counts.Where(c => c.Status == TaskStatusList.Assigned).ToList();
            var closedTasks = counts.Where(c => c.Status == TaskStatusList.Closed).ToList();
            return new UserTasksSummaryDto(tasksTotal.Count, holdTasks.Count, openTasks.Count, closedTasks.Count, averageResolutionTime);
        }

        public async Task ReassignTaskAsync(Guid id, string newUserId)
        {
            logger.LogInformation("Reassigning task {TaskId} to user {NewUserId}", id, newUserId);
            var task = await context.EmailTasks.FindAsync(id) ??
            throw new ResourceNotFoundException("Task", id);
            task.AssignToUser(newUserId);
        }
        public async Task ReOpenTaskAsync(Guid id)
        {
            var task = await context.EmailTasks.FindAsync(id) ??
            throw new ResourceNotFoundException("Task", id);
            task.ReOpenTask();
        }
    }
}