using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailInboxRepository
    {
        Task<EmailInbox> GetEmailById (Guid id);
        Task<EmailInbox> GetEmailByMessageId (string messageId);
        Task<IEnumerable<EmailInbox>> GetEmailsByOrganisation(Guid organisationId);
        Task<IEnumerable<EmailInbox>> GetEmailsByCategory(Guid categoryId);
        Task<IEnumerable<EmailInbox>> GetEmailsByAssignedUser(string userId);
        Task DeleteEmail (Guid id);
        Task ChangeEmailCategory (Guid id, Guid newCategoryId);
        Task ChangeCategoryForBulkEmails(Guid oldCategoryId,Guid newCategoryId);
        Task CreateEmail(EmailInbox emailInbox);
    }
}