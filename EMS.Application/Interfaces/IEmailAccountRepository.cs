using EMS.Application.Dtos.EmailAccounts;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailAccountRepository
    {
        Task CreateEmailAccountAsync(EmailAccount emailAccount);
        Task DeleteAsync(Guid id);
        Task ChangePasswordAsync(Guid id, string oldPassword, string newPassword);
        Task ChangeApplicationSecretAsync(Guid id, string oldSecret, string newSecret);
        Task<EmailAccount?> GetEmailAccountByIdAsync(Guid id);
        Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync();
        Task MarkAsValidated (Guid id);
        Task MarkAsInvalidated (Guid id);
        Task<IEnumerable<EmailAccount>> GetAllValidatedEmailAccountsAsync();
        
    }
}
