using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailRepository(ApplicationDbContext context) : IEmailRepository
    {
        public async Task AssignNewEmailCategory(Guid id, Guid newCategoryId)
        {
             var email = await context.Emails.FindAsync(id)??throw new ResourceNotFoundException("Email",id);
             email.ChangeStatus(EmailStatus.Categorized);
             email.ChangeCategory(newCategoryId);
             await context.SaveChangesAsync();
        }

        public async Task ChangeCategoryForBulkEmails(Guid oldCategoryId, Guid newCategoryId)
        {
            var emails = await context.Emails
            .Where(e => e.EmailCategoryId == oldCategoryId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(
                e => e.EmailCategoryId, newCategoryId
            ));

        }

        public async Task ChangeEmailCategory(Guid id, Guid newCategoryId)
        {
            var emailCategory = await context.Emails.FindAsync(id)
            ?? throw new ResourceNotFoundException("Email category",id);
            emailCategory.ChangeCategory(newCategoryId);
            await context.SaveChangesAsync();
        }

        public async Task ChangeEmailStatus(Guid id, EmailStatus newStatus)
        {
            var email = await context.Emails.FindAsync(id)??throw new ResourceNotFoundException("Email",id);
            email.ChangeStatus(newStatus);
            await context.SaveChangesAsync();
        }

        public async Task CreateEmail(Email email)
        {
            context.Add(email);
            await context.SaveChangesAsync();
        }

        public async Task DeleteEmail(Guid id)
        {
            var affectedRows = await context.Emails.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                throw new ResourceNotFoundException("Email", id);
            }
        }

        public async Task EmailAssignedAction(Guid emailId)
        {
            var email = await context.Emails.FindAsync(emailId)??throw new ResourceNotFoundException("Email",emailId);
            email.NewEmailAssigned();
            await context.SaveChangesAsync();
        }

        public async Task<Email> GetEmailById(Guid id)
        {
            return await context.Emails.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("Email", id);
        }

        public async Task<Email> GetEmailByMessageId(string messageId)
        {
            return await context.Emails.Where(e => e.ExternalMessageId == messageId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Email>> GetEmailsByCategory(Guid categoryId)
        {
            return await context.Emails.Where(e => e.EmailCategoryId == categoryId).ToListAsync();

        }

        public async Task<IEnumerable<Email>> GetEmailsByEmailAccount(Guid emailAccountId)
        {
            return await context.Emails.Where(e => e.EmailAccountId == emailAccountId).ToListAsync();
        }

        public async Task<IEnumerable<Email>> GetEmailsPendingAssignment(Guid emailAccountId)
        {
            return await context.Emails.Where(e=>e.IsAssigned==false && e.EmailAccountId==emailAccountId  &&  e.EmailCategoryId != null).ToListAsync();
        }

        public async Task<IEnumerable<Email>> GetNewEmailsByEmailAccount(Guid emailAccountId)
        {
            return await context.Emails.Where(e => e.EmailAccountId == emailAccountId && e.Status == EmailStatus.New).ToListAsync();
        }
    }
}