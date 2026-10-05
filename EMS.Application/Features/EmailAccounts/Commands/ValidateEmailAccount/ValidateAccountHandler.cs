using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.EmailAccounts.Commands.ValidateEmailAccount
{
    public class ValidateAccountHandler(IEnumerable<IEmailProviderValidator> validators, IEmailAccountRepository emailConfigurationRepository, IEncryptionService encryptionService) : ICommandHandler<ValidateAccountCommand, bool>
    {


        public async Task<bool> Handle(ValidateAccountCommand request, CancellationToken cancellationToken)
        {

            var emailAccount = await emailConfigurationRepository.GetEmailAccountByIdAsync(request.EmailAccountId) ?? throw new ResourceNotFoundException($"Email account", request.EmailAccountId);
            var validator = validators.FirstOrDefault(v => v.EmailType == emailAccount.EmailType)
       ?? throw new InvalidOperationException($"No validator found for {emailAccount.EmailType}");
       var clientSecret = string.Empty;
       var password = string.Empty;
            if (emailAccount.EmailType == Domain.Enums.EmailType.Office365)
            {
                if(string.IsNullOrEmpty(emailAccount.ClientSecret) || string.IsNullOrEmpty(emailAccount.ClientId) || string.IsNullOrEmpty(emailAccount.TenantId))
                {
                    throw new ArgumentException("Client secret, client ID, and tenant ID cannot be null or empty for Office365 email accounts.");
                }
                clientSecret = encryptionService.DecryptData(emailAccount.ClientSecret);
            }
            else
            {
                if(string.IsNullOrEmpty(emailAccount.Password))
                {
                    throw new ArgumentException("Password cannot be null or empty for non-Office365 email accounts.", nameof(emailAccount.Password));
                }
                password = encryptionService.DecryptData(emailAccount.Password);
            }
            var result = await validator.IsEmailConfigValidAsync(emailAccount.Id, emailAccount.EmailAddress, emailAccount.EmailType, password, emailAccount.ClientId, clientSecret, emailAccount.TenantId);
            if (result == true)
            {
                await emailConfigurationRepository.MarkAsValidated(emailAccount.Id);
            }
            else
            {
                await emailConfigurationRepository.MarkAsInvalidated(emailAccount.Id);
            }
            return result;


        }
    }
}
