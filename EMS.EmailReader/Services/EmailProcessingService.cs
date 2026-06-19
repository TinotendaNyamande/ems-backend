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
        IEmailAccountRepository emailAccountRepository,
         IEmailRepository emailRepository,
         IEncryptionService encryptionService,
         IOrganisationRepository organisationRepository,
         IEmailCategoryRepository emailCategoryRepository,
         IEmailCategorizer emailCategorizer,
         ILogger<EmailProcessingService> logger
         ) : IEmailProcessingService
    {

        public Task AssignEmailToUser()
        {
            throw new NotImplementedException();
        }

        // public async Task DetermineEmailCategory()
        // {
        //     logger.LogInformation("Start: Categorize  email service");
        //     var emailAccounts = await emailAccountRepository.GetAllValidatedAsync();
        //     logger.LogInformation("Found {count} number of validated mailboxes", emailAccounts.Count());
        //     if (emailAccounts.Any())
        //     {

        //         foreach (var emailAccount in emailAccounts)
        //         {
        //             var organisation = await organisationRepository.GetByIdAsync(emailAccount.OrganisationId);
        //             var categories = await emailCategoryRepository.GetEmailCategoriesAsync(organisation.Id);
        //             var categoriesList = categories
        //                 .Select(c => c.CategoryName)
        //                 .ToArray();
        //             var emails = await emailRepository.GetNewEmailsByEmailAccount(emailAccount.Id);
        //             foreach (var email in emails)
        //             {
        //                 var category = await emailCategorizer.CategorizeAsync(email.Subject, email.Body, categoriesList);
        //                 var newEmailCategory = categories
        //                     .FirstOrDefault(p =>
        //                         p.CategoryName.Equals(
        //                             category.Category,
        //                             StringComparison.OrdinalIgnoreCase));
        //                 await emailRepository.ChangeEmailCategory(email.Id, newEmailCategory.Id);

        //             };
        //         }
        //     }
        // }
public async Task DetermineEmailCategory()
{
    logger.LogInformation("Start: Categorize email service");

    var emailAccounts = await emailAccountRepository
        .GetAllValidatedAsync();

    logger.LogInformation(
        "Found {count} validated mailboxes",
        emailAccounts.Count());

    foreach (var emailAccount in emailAccounts)
    {
        var organisation =
            await organisationRepository
                .GetByIdAsync(emailAccount.OrganisationId);

        var categories =
            await emailCategoryRepository
                .GetEmailCategoriesAsync(organisation.Id);

        var categoryNames = categories
            .Select(c => c.CategoryName)
            .ToArray();

        var emails =
            await emailRepository
                .GetNewEmailsByEmailAccount(emailAccount.Id);

        var tasks = emails.Select(async email =>
        {
            try
            {
                var result = await emailCategorizer.CategorizeAsync(
                    email.Subject,
                    email.Body,
                    categoryNames);

                var matchedCategory = categories
                    .FirstOrDefault(c =>
                        c.CategoryName.Equals(
                            result.Category,
                            StringComparison.OrdinalIgnoreCase));

                if (matchedCategory == null)
                {
                    logger.LogWarning(
                        "No category match found for AI category: {Category}",
                        result.Category);

                    return;
                }

                await emailRepository.AssignNewEmailCategory(
                    email.Id,
                    matchedCategory.Id);

                logger.LogInformation(
                    "Email {EmailId} categorized as {Category}",
                    email.Id,
                    matchedCategory.CategoryName);
            }
            catch(Exception ex)
            {
                logger.LogError(
                    ex,
                    "Error categorizing email {EmailId}",
                    email.Id);
            }
        });

        await Task.WhenAll(tasks);
    }
}

        public async Task ReadEmailsFromInbox()
        {
            logger.LogInformation("Start: Read from email service");
            var emailAccounts = await emailAccountRepository.GetAllValidatedAsync();
            logger.LogInformation("Found {count} number of validated mailboxes", emailAccounts.Count());
            if (emailAccounts.Any())
            {
                foreach (var emailAccount in emailAccounts)
                {
                    logger.LogInformation("Processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);
                    var emails = await emailRepository.GetEmailsByEmailAccount(emailAccount.Id);
                    foreach (var email in emails)
                    {
                        logger.LogInformation("Processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);
                        if (emailAccount.EmailType == EmailType.Office365)
                        {
                            await this.ReadOffice365InboxService(emailAccount);

                        }
                        else
                        {
                            await ReadGmailInboxService(emailAccount);
                        }
                        logger.LogInformation("Finished processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);
                    }

                }
            }

        }
        private async Task ReadGmailInboxService(EmailAccount emailAccount)
        {
            try
            {
                logger.LogInformation("Hashed password value {password}", emailAccount.Password);
                var password = encryptionService.DescryptData(emailAccount.Password);
                logger.LogInformation("Connecting using password {password}", password);
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
                                    message.Subject,
                                    message.TextBody,
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


        }
        private async Task ReadOffice365InboxService(EmailAccount emailAccount)
        {
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
                    logger.LogInformation("Start: Reading individual messages for Office 365 account {id}", emailAccount.Id);
                    logger.LogInformation("Start: processing message with message Id {id}", message.Id);
                    var isProcessed = await IsProcessed(message.Id);
                    if (!isProcessed)
                    {
                        logger.LogInformation("Message with ID {id} has not been processed before", message.Id);
                        var newEmail = new Email
                         (
                             message.From?.EmailAddress?.Address,
                             string.Join(", ", message.ToRecipients.Select(r => r.EmailAddress?.Address)),
                             message.Subject,
                             message.Body?.Content,
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