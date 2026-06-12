using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Infrastructure.Repository.EmailValidation
{
    internal class OutlookValidator : IEmailProviderValidator
    {
        public EmailType EmailType => EmailType.Outlook;
        public async Task<bool> IsEmailConfigValidAsync(ValidationAndTestEmailDto config)
        {
            try
            {
                using var client = new MailKit.Net.Smtp.SmtpClient();

                await client.ConnectAsync("smtp.office365.com", 587, false);

                await client.AuthenticateAsync(
                    config.EmailAddress,
                    config.Password
                );

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
