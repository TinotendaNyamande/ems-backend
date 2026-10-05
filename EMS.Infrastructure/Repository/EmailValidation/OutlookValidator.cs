using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Infrastructure.Repository.EmailValidation
{
    internal class OutlookValidator : IEmailProviderValidator
    {
        public EmailType EmailType => EmailType.Outlook;
        public async Task<bool> IsEmailConfigValidAsync(Guid Id, string EmailAddress, EmailType EmailType, string? Password, string? ClientId, string? ClientSecret, string? TenantId)
        {
            try
            {
                using var client = new MailKit.Net.Smtp.SmtpClient();

                await client.ConnectAsync("smtp.office365.com", 587, false);
                if (!string.IsNullOrEmpty(Password))
                {
                    await client.AuthenticateAsync(
                        EmailAddress,
                        Password
                    );
                }
                else if (!string.IsNullOrEmpty(ClientId) && !string.IsNullOrEmpty(ClientSecret) && !string.IsNullOrEmpty(TenantId))
                {
                    var oauth2 = new MailKit.Security.SaslMechanismOAuth2(EmailAddress, ClientSecret);
                    await client.AuthenticateAsync(oauth2);
                }
                else
                {
                    throw new ArgumentException("Either Password or ClientId, ClientSecret, and TenantId must be provided.");
                }   

                // await client.AuthenticateAsync(
                //     EmailAddress,
                //     Password
                // );

                await client.DisconnectAsync(true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
