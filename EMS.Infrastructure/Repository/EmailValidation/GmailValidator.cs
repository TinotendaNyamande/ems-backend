using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Exceptions;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using MailKit.Security;

namespace EMS.Infrastructure.Repository.EmailValidation
{
    internal class GmailValidator (ApplicationDbContext con): IEmailProviderValidator
    {
        public EmailType EmailType => EmailType.Gmail;

        public async Task<bool> IsEmailConfigValidAsync(MailBoxConfig config)
        {
            try
            {
                using var client = new MailKit.Net.Smtp.SmtpClient();

                await client.ConnectAsync(
                    "smtp.gmail.com",
                    587,
                    SecureSocketOptions.StartTls
                );

                await client.AuthenticateAsync(
                    config.EmailAddress,
                    config.Password
                );

                var authenticated = client.IsAuthenticated;

                await client.DisconnectAsync(true);

                return authenticated;
            }
            catch (MailKit.Security.AuthenticationException)
            {
                return false;
            }
            catch (EmailValidationException ex)
            {
                throw new Exception(
                    $"Failed to validate Gmail configuration: {ex.Message}"
                );
            }
        }
    }
}