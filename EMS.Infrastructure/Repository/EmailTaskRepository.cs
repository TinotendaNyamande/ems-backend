using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailTaskRepository(ApplicationDbContext context) : IEmailTasksRepository
    {

        public async Task ChangeTaskStatusAsync(Guid id, TaskStatusList newStatus)
        {
            var task = await context.EmailTasks.Where(t => t.Id == id).FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("Task", id);
            task.ChangeStatus(newStatus);
            await context.SaveChangesAsync();
        }

        public async Task CloseTaskAsync(Guid id, string additionalInformation)
        {
            var task = await context.EmailTasks.Where(t => t.Id == id).FirstOrDefaultAsync() ??
             throw new ResourceNotFoundException("Task", id);
            task.CloseTask(additionalInformation);
            await context.SaveChangesAsync();
        }

        public async Task CreateTaskAsync(EmailTask emailTask)
        {
            context.Add(emailTask);
            await context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var affectedRows = await context.EmailTasks.Where(t => t.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                throw new ResourceNotFoundException("Task", id);
            }
        }

        public async Task EditAdditionalInformation(Guid id, string additionalInfo)
        {
            var task = await context.EmailTasks.Where(t => t.Id == id).FirstOrDefaultAsync() ??
             throw new ResourceNotFoundException("Task", id);
            task.EditAdditionalInfo(additionalInfo);
            await context.SaveChangesAsync();
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

        public async Task<IEnumerable<GetTasksDto>> GetTasksByUserId(string userId, TaskStatusList? status)
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

        public async Task<IEnumerable<GetTasksDto>> GetTasksForOrganisation(Guid organisationId, TaskStatusList? status)
        {

            var query =
                from tasks in context.EmailTasks
                join user in context.Users
                on tasks.AssignedToUser equals user.Id
                join emails in context.Emails
                on tasks.EmailId equals emails.Id
                join emailsAccounts in context.EmailAccounts
                on emails.EmailAccountId equals emailsAccounts.Id
                join org in context.Organisations
                on emailsAccounts.OrganisationId equals org.Id
                join category in context.EmailCategories
                on emails.EmailCategoryId equals category.Id
                where org.Id == organisationId
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

        public async Task ReassignTaskAsync(Guid id, string newUserId)
        {
            var task = await context.EmailTasks.Where(t => t.Id == id).AsNoTracking().FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("Task", id);
            task.AssignToUser(newUserId);
            await context.SaveChangesAsync();
        }
    }
}