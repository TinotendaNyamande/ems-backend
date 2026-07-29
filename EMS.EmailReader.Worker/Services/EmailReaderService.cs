using EMS.Application.Interfaces;
using EMS.Domain.Models;
using Azure.Identity;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using EMS.Domain.Enums;

namespace EMS.EmailReader.Worker.Services
{
    internal class EmailReaderProcessor(
         IEmailRepository emailRepository,
         IEncryptionService encryptionService,
         IEmailAccountRepository emailAccountRepository,
         ILogger<EmailReaderProcessor> logger
    ) : IEmailReaderService
    {
        public async Task<int> ProcessAsync(CancellationToken cancellationToken)
        {
            var tasksCount = 0;
            logger.LogInformation("Start: Read from email service");
            var emailAccounts = await emailAccountRepository.GetAllValidatedAsync();
            logger.LogInformation("Found {count} number of validated mailboxes", emailAccounts.Count());
            if (emailAccounts.Any())
            {
                foreach (var emailAccount in emailAccounts)
                {
                    logger.LogInformation("Processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);

                    logger.LogInformation("Processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);
                    if (emailAccount.EmailType == EmailType.Office365)
                    {
                        var office365Count = await ReadOffice365InboxService(emailAccount);
                        tasksCount += office365Count;
                    }
                    else
                    {
                        var gmailCount = await ReadGmailInboxService(emailAccount);
                        tasksCount += gmailCount;
                    }
                    
                    logger.LogInformation("Finished processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);


                }
            }
            return tasksCount;

        }
        private async Task<int> ReadGmailInboxService(EmailAccount emailAccount)
        {
            var gmailCount = 0;
            try
            {
                var hashedPassword = emailAccount.Password;
                if (hashedPassword == null)
                {
                    throw new Exception("Email account does not have password");
                }
                var password = encryptionService.DescryptData(hashedPassword);
                using var client = new ImapClient();
                logger.LogInformation("Attempting to connect to client");
                await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                logger.LogInformation("Connected to client successfully");
                logger.LogInformation("Attempting to authenticate");
                await client.AuthenticateAsync(emailAccount.EmailAddress, password);
                logger.LogInformation("Successfully connected to Gmail account for account id {id}", emailAccount.Id);
                var inbox = client.Inbox;
                await inbox.OpenAsync(MailKit.FolderAccess.ReadWrite);
                var unreadUids = await inbox.SearchAsync(MailKit.Search.SearchQuery.NotSeen);
                logger.LogInformation("Successfully connected to Gmail account inbox for account id {id}", emailAccount.Id);
                foreach (var uid in unreadUids)
                {
                    gmailCount++;
                    logger.LogInformation("Start: Reading individual messages for Gmail account {id}", emailAccount.Id);
                    logger.LogInformation("Start: processing message with uid {uid}", uid);

                    var message = await inbox.GetMessageAsync(uid);
                    var isProcessed = await IsProcessed(message.MessageId);
                    logger.LogInformation("Message : {msg}", message);
                    if (!isProcessed)
                    {
                        logger.LogInformation("Message with ID {id} has not been processed before", message.MessageId);
                        var newEmail = new Email
                                (
                                    message.From.ToString(),
                                    message.To.ToString(),
                                    message.Subject ?? string.Empty,
                                    message.TextBody ?? string.Empty,
                                    emailAccount.Id,
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
                logger.LogError("Disconnecting account with Id {id} after finishing reading messages", emailAccount.Id);
                


            }
            catch (Exception ex)
            {
                logger.LogError("An error occured why trying to read inbox");
                logger.LogError("Error Message : {message}", ex.Message);
                logger.LogError("Stack Trace: {stack}", ex.StackTrace);
                logger.LogError("Full Error: {ex}", ex);
            }
            return gmailCount;
        }

        private async Task<int> ReadOffice365InboxService(EmailAccount emailAccount)
        {
            var office365Count = 0;
            try
            {
                var clientSecret = encryptionService.DescryptData(emailAccount.ClientSecret);
                var credential = new ClientSecretCredential(
                  tenantId: emailAccount.TenantId,
                  clientId: emailAccount.ClientId,
                  clientSecret: clientSecret
              );
                var graphClient = new GraphServiceClient(credential);
                logger.LogInformation("Successfully connected to Office 365 account for account id {id}", emailAccount.Id);
                var messages = await graphClient.Users[emailAccount.EmailAddress].Messages.GetAsync(config =>
                {
                    config.QueryParameters.Filter = "isRead eq false";
                    config.QueryParameters.Top = 50;
                });
                logger.LogInformation("Successfully connected to Office 365 account inbox for account id {id}", emailAccount.Id);

                foreach (var message in messages.Value)
                {
                    office365Count++;
                    logger.LogInformation("Start: Reading individual messages for Office 365 account {id}", emailAccount.Id);
                    logger.LogInformation("Start: processing message with message Id {id}", message.Id);
                    var isProcessed = await IsProcessed(message.Id);
                    if (!isProcessed)
                    {
                        logger.LogInformation("Message with ID {id} has not been processed before", message.Id);
                        var newEmail = new Email
                          (
                              message.From?.EmailAddress?.Address ?? string.Empty,
                              string.Join(", ", message.ToRecipients.Select(r => r.EmailAddress?.Address)),
                              message.Subject ?? string.Empty,
                              message.Body?.Content ?? string.Empty,
                              emailAccount.Id,
                              message.Id
                          );
                        await SaveEmail(newEmail);
                    }
                    else
                    {
                        logger.LogInformation("Skipping Message with ID {id} as it has already been processed", message.Id);
                    }
                    logger.LogInformation("Marking message with ID {id} as read", message.Id);
                    await graphClient.Users[emailAccount.EmailAddress].Messages[message.Id].PatchAsync(new Message
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
            return office365Count;

        }
        private async Task<bool> IsProcessed(string messageId)
        {
            var isProcessed = await emailRepository.GetEmailByMessageId(messageId);
            return isProcessed != null;
        }
        private async Task SaveEmail(Email email)
        {
            await emailRepository.CreateEmail(email);
        }
    }
}