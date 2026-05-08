using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Exceptions;
using EMS.Domain.Models;
using Microsoft.Identity.Client;

namespace EMS.Infrastructure.Repository.EmailValidation
{
    internal class Office365Validator : IEmailProviderValidator
    {
        public EmailType EmailType => EmailType.Office365;
        public async Task<bool> IsEmailConfigValidAsync(MailBoxConfig config)
        {
            try
            {
                var app = ConfidentialClientApplicationBuilder
                    .Create(config.ClientId)
                    .WithClientSecret(config.ClientSecret)
                    .WithAuthority($"https://login.microsoftonline.com/{config.TenantId}")
                    .Build();

                var result = await app.AcquireTokenForClient(
                    new[] { "https://graph.microsoft.com/.default" }
                ).ExecuteAsync();
                return result !=null && !string.IsNullOrEmpty(result.AccessToken);

            }
            catch(MsalServiceException)
            {
                return false;
            }
            catch (MsalClientException)
            {
                return false;
            }
            catch(EmailValidationException ex)
            {
                throw new Exception($"Failed to validate Office365 configuration: {ex.Message}");
            }
           
        }
    }
}
