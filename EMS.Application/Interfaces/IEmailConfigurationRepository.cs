using EMS.Application.Dtos.EmailConfigs;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailConfigurationRepository
    {
        Task CreateEmailAccountAsync(MailBoxConfig mailBoxConfig);
        Task DeleteAsync(Guid id);
        Task ChangePasswordAsync(Guid id, ChangePasswordDto changePasswordDto);
        Task ChangeApplicationSecretAsync(Guid id, ChangeClientSecretDto changeClientSecretDto);
        Task<MailBoxConfig> GetEmailAccountAsync(Guid id);
        Task<IEnumerable<MailBoxConfig>> GetEmailAccountsForOrganisationAsync(Guid organisationId);
        Task MarkAsValidated (Guid id);
        Task MarkAsInvalidated (Guid id);
    }
}
