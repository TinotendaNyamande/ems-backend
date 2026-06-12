using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace EMS.Infrastructure.Repository
{
    internal class EmailInboxRepository(ApplicationDbContext context) : IEmailInboxRepository
    {
        public async Task ChangeCategoryForBulkEmails(Guid oldCategoryId, Guid newCategoryId)
        {
            var emails = await context.EmailInboxes
            .Where(e => e.EmailCategoryId == oldCategoryId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(
                e => e.EmailCategoryId, newCategoryId
            ));

        }

        public Task ChangeEmailCategory(Guid id, Guid newCategoryId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateEmail(EmailInbox emailInbox)
        {
            context.Add(emailInbox);
            await context.SaveChangesAsync();
        }

        public Task DeleteEmail(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EmailInbox> GetEmailById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<EmailInbox> GetEmailByMessageId(string messageId)
        {
            return await context.EmailInboxes.Where(e => e.ExternalMessageId == messageId).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<EmailInbox>> GetEmailsByAssignedUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmailInbox>> GetEmailsByCategory(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmailInbox>> GetEmailsByOrganisation(Guid organisationId)
        {
            throw new NotImplementedException();
        }
    }
}