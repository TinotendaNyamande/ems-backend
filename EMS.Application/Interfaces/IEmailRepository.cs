using EMS.Application.Dtos.Emails;
using EMS.Application.Features.Emails.Commands.CreateEmail;
using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailRepository
    {
        Task<IEnumerable<IncomingEmailDto>> GetUnreadMessagesFromGmailInboxAsync(Guid id,string emailAddress,string password,CancellationToken cancellationToken);
        Task<IEnumerable<IncomingEmailDto>> GetUnreadMessagesFromOffice365InboxAsync(Guid id,string tenantId,string clientId,string clientSecret,string emailAddress,CancellationToken cancellationToken);
        Task<Email> GetEmailByIdAsync(Guid id);
        Task<Email> GetEmailByMessageIdAsync(string messageId);
        Task<IEnumerable<Email>> GetEmailsByEmailAccountAsync(Guid emailAccountId);
        Task<IEnumerable<Email>> GetEmailsByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Email>> GetEmailsPendingAssignmentAsync(Guid emailAccountId);
        Task DeleteEmailAsync(Guid id);
        Task ChangeEmailCategoryAsync(Guid id, Guid newCategoryId);
        Task ChangeCategoryForBulkEmailsAsync(Guid oldCategoryId, Guid newCategoryId);
        Task<Email> CreateEmailAsync(Email email);
        Task<IEnumerable<Email>> GetNewEmailsByEmailAccountAsync(Guid emailAccountId);
        Task ChangeEmailStatusAsync(Guid id,EmailStatus newStatus);
        Task AssignEmailCategoryAsync(Guid id,Guid newCategoryId);
        Task MarkEmailAsAssignedAsync (Guid emailId);
        Task<IEnumerable<Email>> GetEmailsPendingCategorizationAsync();
    }
}