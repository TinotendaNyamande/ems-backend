using EMS.Application.Abstractions;
using EMS.Application.Interfaces;

using EMS.Domain.Exceptions;

namespace EMS.Application.Features.EmailAccounts.Commands.TestEmailAccount
{
    internal class TestEmailHandler(IEmailSender emailSender,IEmailAccountRepository emailAccountRepository,IEncryptionService encryptionService): ICommandHandler<TestEmailAccountCommand>
    {
        public async Task Handle(TestEmailAccountCommand request, CancellationToken cancellationToken)
        {
            var emailAccount = await emailAccountRepository.GetEmailAccountByIdAsync(request.EmailAccountId) ?? throw new ResourceNotFoundException($"Email account", request.EmailAccountId);
            var password = string.Empty;
            var clientSecret = string.Empty;
            if (emailAccount.EmailType==Domain.Enums.EmailType.Office365)
            {
                if(string.IsNullOrEmpty(emailAccount.ClientSecret)||string.IsNullOrEmpty(emailAccount.ClientId)||string.IsNullOrEmpty(emailAccount.TenantId))
                {
                    throw new ArgumentException("Client secret, client ID, and tenant ID cannot be null or empty for Office365 email accounts.", nameof(emailAccount.ClientSecret));
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
            await emailSender.SendTestEmailAsync(emailAccount.EmailType, emailAccount.EmailAddress, password, emailAccount.ClientId, clientSecret, emailAccount.TenantId, request.ToEmail);
        }
    }
}
