using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailRepository
    {
        Task<Email> GetEmailById(Guid id);
        Task<Email> GetEmailByMessageId(string messageId);
        Task<IEnumerable<Email>> GetEmailsByEmailAccount(Guid emailAccountId);
        Task<IEnumerable<Email>> GetEmailsByCategory(Guid categoryId);
        Task<IEnumerable<Email>> GetEmailsByAssignedUser(string userId);
        Task DeleteEmail(Guid id);
        Task ChangeEmailCategory(Guid id, Guid newCategoryId);
        Task ChangeCategoryForBulkEmails(Guid oldCategoryId, Guid newCategoryId);
        Task CreateEmail(Email email);
        Task<IEnumerable<Email>> GetNewEmailsByEmailAccount(Guid emailAccountId);
        Task ChangeEmailStatus(Guid id,EmailStatus newStatus);
        Task AssignNewEmailCategory(Guid id,Guid newCategoryId);
    }
}