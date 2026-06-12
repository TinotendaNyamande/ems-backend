using Azure.Identity;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Serilog;

namespace EMS.EmailReader.Services
{
    internal class EmailProcessingService(
        IEmailConfigurationRepository emailConfigurationRepository,
         IEnumerable<IEmailProviderValidator> validators,
         IOrganisationRepository organisationRepository,
         IEmailInboxRepository emailInboxRepository,
         IEncryptionService encryptionService,
         ILogger<EmailProcessingService> logger
         ) : IEmailProcessingService
    {

        public Task AssignEmailToUser()
        {
            throw new NotImplementedException();
        }

        public Task DetermineEmailCategory()
        {
            throw new NotImplementedException();
        }

        public async Task ReadEmailsFromInbox()
        {
            logger.LogInformation("Start: Read from email service");

            var mailBoxConfigs = await emailConfigurationRepository.GetAllValidatedAsync();
            logger.LogInformation("Found {count} number of validated mailboxes", mailBoxConfigs.Count());
            if (mailBoxConfigs.Any())
            {
                foreach (var mailBoxConfig in mailBoxConfigs)
                {
                    logger.LogInformation("Processing mailbox {id} of type {type}", mailBoxConfig.Id, mailBoxConfig.EmailType);
                    if (mailBoxConfig.EmailType == EmailType.Office365)
                    {

                        await ReadOffice365InboxService(mailBoxConfig);

                    }
                    else
                    {
                        await ReadGmailInboxService(mailBoxConfig);
                    }
                    logger.LogInformation("Finished processing mailbox {id} of type {type}", mailBoxConfig.Id, mailBoxConfig.EmailType);
                }
            }

        }
        private async Task ReadGmailInboxService(MailBoxConfig mailBoxConfig)
        {
            try
            {
                var password = encryptionService.DescryptData(mailBoxConfig.Password);
                using var client = new ImapClient();
                await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(mailBoxConfig.EmailAddress, password);
                logger.LogInformation("Successfully connected to Gmail account for account id {id}", mailBoxConfig.Id);
                var inbox = client.Inbox;
                await inbox.OpenAsync(MailKit.FolderAccess.ReadWrite);
                var unreadUids = await inbox.SearchAsync(MailKit.Search.SearchQuery.NotSeen);
                logger.LogInformation("Successfully connected to Gmail account inbox for account id {id}", mailBoxConfig.Id);
                foreach (var uid in unreadUids)
                {
                    logger.LogInformation("Start: Reading individual messages for Gmail account {id}", mailBoxConfig.Id);
                    logger.LogInformation("Start: processing message with uid {uid}", uid);

                    var message = await inbox.GetMessageAsync(uid);
                    var isProcessed = await IsProcessed(message.MessageId);
                    if (!isProcessed)
                    {
                        logger.LogInformation("Message with ID {id} has not been processed before", message.MessageId);
                        var newEmail = new EmailInbox
                                (
                                    message.From.ToString(),
                                    message.To.ToString(),
                                    message.Subject,
                                    message.TextBody.ToString(),
                                    mailBoxConfig.Id,
                                    message.MessageId
                                );
                        await SaveEmail(newEmail);

                    }
                    else
                    {
                        logger.LogInformation("Skipping Message with ID {id} as it has already been processed", message.MessageId);
                    }
                    logger.LogInformation("Marking message with ID {id} as read", message.MessageId);
                    await inbox.AddFlagsAsync(
                            uid,
                            MessageFlags.Seen,
                            true
                        );
                }

                await client.DisconnectAsync(true);
                logger.LogError("Disconnecting account with Id {id} after finishing reading messages", mailBoxConfig.Id);


            }
            catch (Exception ex)
            {
                logger.LogError("An error occured why trying to read inbox");
                logger.LogError("Error Message : {message}", ex.Message);
                logger.LogError("Stack Trace: {stack}", ex.StackTrace);
                logger.LogError("Full Error: {ex}", ex);
            }


        }
        private async Task ReadOffice365InboxService(MailBoxConfig mailBoxConfig)
        {
            try
            {
                var clientSecret = encryptionService.DescryptData(mailBoxConfig.ClientSecret);
                var credential = new ClientSecretCredential(
                  tenantId: mailBoxConfig.TenantId,
                  clientId: mailBoxConfig.ClientId,
                  clientSecret: clientSecret
              );
                var graphClient = new GraphServiceClient(credential);
                logger.LogInformation("Successfully connected to Office 365 account for account id {id}", mailBoxConfig.Id);
                var messages = await graphClient.Users[mailBoxConfig.EmailAddress].Messages.GetAsync(config =>
                {
                    config.QueryParameters.Filter = "isRead eq false";
                    config.QueryParameters.Top = 50;
                });
                logger.LogInformation("Successfully connected to Office 365 account inbox for account id {id}", mailBoxConfig.Id);

                foreach (var message in messages.Value)
                {
                    logger.LogInformation("Start: Reading individual messages for Office 365 account {id}", mailBoxConfig.Id);
                    logger.LogInformation("Start: processing message with message Id {id}", message.Id);
                    var isProcessed = await IsProcessed(message.Id);
                    if (!isProcessed)
                    {
                        logger.LogInformation("Message with ID {id} has not been processed before", message.Id);
                        var newEmail = new EmailInbox
                         (
                             message.From?.EmailAddress?.Address,
                             message.ToRecipients.Select(r => r.EmailAddress?.Address).ToString(),
                             message.Subject,
                             message.Body.Content,
                             mailBoxConfig.Id,
                             message.Id
                         );
                        await SaveEmail(newEmail);
                    }
                    else
                    {
                        logger.LogInformation("Skipping Message with ID {id} as it has already been processed", message.Id);
                    }
                    logger.LogInformation("Marking message with ID {id} as read", message.Id);
                    await graphClient.Users[mailBoxConfig.EmailAddress].Messages[message.Id].PatchAsync(new Message
                    {
                        IsRead = true
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("An error occured why trying to read inbox");
                logger.LogError("Error Message : {message}", ex.Message);
                logger.LogError("Stack Trace: {stack}", ex.StackTrace);
                logger.LogError("Full Error: {ex}", ex);
            }

        }
        private async Task<bool> IsProcessed(string messageId)
        {
            var isProcessed = await emailInboxRepository.GetEmailByMessageId(messageId);
            return isProcessed != null;
        }
        private async Task SaveEmail(EmailInbox emailInbox)
        {
            await emailInboxRepository.CreateEmail(emailInbox);
        }
    }
}