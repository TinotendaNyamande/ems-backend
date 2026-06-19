using EMS.Application.Dtos.EmailAccounts;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailAccountRepository
    {
        Task CreateEmailAccountAsync(EmailAccount emailAccount);
        Task DeleteAsync(Guid id);
        Task ChangePasswordAsync(Guid id, ChangeEmailPasswordDto changePasswordDto);
        Task ChangeApplicationSecretAsync(Guid id, ChangeClientSecretDto changeClientSecretDto);
        Task<EmailAccount> GetEmailAccountAsync(Guid id);
        Task<IEnumerable<EmailAccount>> GetEmailAccountsForOrganisationAsync(Guid organisationId);
        Task MarkAsValidated (Guid id);
        Task MarkAsInvalidated (Guid id);
        Task<IEnumerable<EmailAccount>> GetAllValidatedAsync();
        
    }
}
