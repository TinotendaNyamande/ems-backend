using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Exceptions;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using MailKit.Security;
using Microsoft.Extensions.Logging;

namespace EMS.Infrastructure.Repository.EmailValidation
{
    internal class GmailValidator (ILogger<GmailValidator> logger): IEmailProviderValidator
    {
        public EmailType EmailType => EmailType.Gmail;

        public async Task<bool> IsEmailConfigValidAsync(ValidationAndTestEmailAccountDto config)
        {
            try
            {
                logger.LogInformation("Trying to validate email {email}",config.EmailAddress);
                using var client = new MailKit.Net.Smtp.SmtpClient();

                await client.ConnectAsync(
                    "smtp.gmail.com",
                    587,
                    SecureSocketOptions.StartTls
                );
                logger.LogInformation("Connected to smtp server");

                await client.AuthenticateAsync(
                    config.EmailAddress,
                    config.Password
                );
                logger.LogInformation("Email details authentication process finished");
                logger.LogInformation("Auth process results {results}",client.IsAuthenticated);

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