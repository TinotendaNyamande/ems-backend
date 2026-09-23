using Azure.Identity;
using EMS.Application.Dtos.Emails;
using EMS.Application.Features.Emails.Commands.CreateEmail;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailRepository(ApplicationDbContext context, ILogger<EmailRepository> logger) : IEmailRepository
    {
        public async Task AssignEmailCategoryAsync(Guid id, Guid newCategoryId)
        {
            var email = await context.Emails.FindAsync(id) ?? throw new ResourceNotFoundException("Email", id);
            email.ChangeStatus(EmailStatus.Categorized);
            email.ChangeCategory(newCategoryId);
            await context.SaveChangesAsync();
        }

        public async Task ChangeCategoryForBulkEmailsAsync(Guid oldCategoryId, Guid newCategoryId)
        {
            var emails = await context.Emails
            .Where(e => e.EmailCategoryId == oldCategoryId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(
                e => e.EmailCategoryId, newCategoryId
            ));

        }

        public async Task ChangeEmailCategoryAsync(Guid id, Guid newCategoryId)
        {
            var emailCategory = await context.Emails.FindAsync(id)
            ?? throw new ResourceNotFoundException("Email category", id);
            emailCategory.ChangeCategory(newCategoryId);
            await context.SaveChangesAsync();
        }

        public async Task ChangeEmailStatusAsync(Guid id, EmailStatus newStatus)
        {
            var email = await context.Emails.FindAsync(id) ?? throw new ResourceNotFoundException("Email", id);
            email.ChangeStatus(newStatus);
            await context.SaveChangesAsync();
        }

        public async Task<Email> CreateEmailAsync(Email email)
        {
            context.Add(email);
            await context.SaveChangesAsync();
            return email;
        }

        public async Task DeleteEmailAsync(Guid id)
        {
            var affectedRows = await context.Emails.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                throw new ResourceNotFoundException("Email", id);
            }
        }

        public async Task MarkEmailAsAssignedAsync(Guid emailId)
        {
            var email = await context.Emails.FindAsync(emailId) ?? throw new ResourceNotFoundException("Email", emailId);
            email.NewEmailAssigned();
            await context.SaveChangesAsync();
        }

        public async Task<Email> GetEmailByIdAsync(Guid id)
        {
            return await context.Emails.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("Email", id);
        }

        public async Task<Email> GetEmailByMessageIdAsync(string messageId)
        {
            return await context.Emails.Where(e => e.ExternalMessageId == messageId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Email>> GetEmailsByCategoryAsync(Guid categoryId)
        {
            return await context.Emails.Where(e => e.EmailCategoryId == categoryId).ToListAsync();

        }

        public async Task<IEnumerable<Email>> GetEmailsByEmailAccountAsync(Guid emailAccountId)
        {
            return await context.Emails.Where(e => e.EmailAccountId == emailAccountId).ToListAsync();
        }

        public async Task<IEnumerable<Email>> GetEmailsPendingAssignmentAsync(Guid emailAccountId)
        {
            return await context.Emails.Where(e => e.IsAssigned == false && e.EmailAccountId == emailAccountId && e.EmailCategoryId != null).ToListAsync();
        }

        public async Task<IEnumerable<Email>> GetEmailsPendingCategorizationAsync()
        {
            return await context.Emails.Where(a => a.EmailCategoryId == null).ToListAsync();
        }

        public async Task<IEnumerable<Email>> GetNewEmailsByEmailAccountAsync(Guid emailAccountId)
        {
            return await context.Emails.Where(e => e.EmailAccountId == emailAccountId && e.Status == EmailStatus.New).ToListAsync();
        }

        public async Task<IEnumerable<IncomingEmailDto>> GetUnreadMessagesFromGmailInboxAsync(Guid id, string emailAddress, string password, CancellationToken cancellationToken)
        {
            
            try
            {
                var newEmails = new List<IncomingEmailDto>();
                using var client = new ImapClient();
                logger.LogInformation("Attempting to connect to client");
                await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect, cancellationToken);
                logger.LogInformation("Connected to client successfully");
                logger.LogInformation("Attempting to authenticate");
                await client.AuthenticateAsync(emailAddress, password, cancellationToken);
                logger.LogInformation("Successfully connected to Gmail account for account id {id}", id);
                var inbox = client.Inbox;
                await inbox.OpenAsync(MailKit.FolderAccess.ReadWrite, cancellationToken);
                var unreadUids = await inbox.SearchAsync(MailKit.Search.SearchQuery.NotSeen);
                logger.LogInformation("Successfully connected to Gmail account inbox for account id {id}", id);
                foreach (var uid in unreadUids)
                {
                    logger.LogInformation("Start: Reading individual messages for Gmail account {id}", id);
                    logger.LogInformation("Start: processing message with uid {uid}", uid);

                    var message = await inbox.GetMessageAsync(uid);
                    logger.LogInformation("Message : {msg}", message);
                    newEmails.Add(new IncomingEmailDto(
                        message.From.ToString(),
                                    message.To.ToString(),
                                    message.Subject ?? string.Empty,
                                    message.TextBody ?? string.Empty,
                                    message.MessageId
                                    ));

                    logger.LogInformation("Marking message with ID {id} as read", message.MessageId);
                    await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true, cancellationToken: cancellationToken);

                }

                await client.DisconnectAsync(true, cancellationToken);
                logger.LogError("Disconnecting account with Id {id} after finishing reading messages", id);
                return newEmails;

            }
            catch (Exception ex)
            {
                logger.LogError("An error occured why trying to read inbox");
                logger.LogError("Error Message : {message}", ex.Message);
                logger.LogError("Stack Trace: {stack}", ex.StackTrace);
                logger.LogError("Full Error: {ex}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<IncomingEmailDto>> GetUnreadMessagesFromOffice365InboxAsync(Guid id,string tenantId,string clientId,string clientSecret,string emailAddress,CancellationToken cancellationToken)
        {
            try
            {
                var newEmails = new List<IncomingEmailDto>();
                var credential = new ClientSecretCredential(
                  tenantId: tenantId,
                  clientId: clientId,
                  clientSecret: clientSecret
              );
                var graphClient = new GraphServiceClient(credential);
                logger.LogInformation("Successfully connected to Office 365 account for account id {id}", id);
                var messages = await graphClient.Users[emailAddress].Messages.GetAsync(config =>
                {
                    config.QueryParameters.Filter = "isRead eq false";
                    config.QueryParameters.Top = 50;
                });
                logger.LogInformation("Successfully connected to Office 365 account inbox for account id {id}", id);

                foreach (var message in messages.Value)
                {
                    logger.LogInformation("Start: Reading individual messages for Office 365 account {id}", id);
                    logger.LogInformation("Start: processing message with message Id {id}", message.Id);

                        logger.LogInformation("Message with ID {id} has not been processed before", message.Id);
                        var emailId = Guid.NewGuid();
                        newEmails.Add(new IncomingEmailDto
                          (
                             message.From?.EmailAddress?.Address ?? string.Empty,
                             string.Join(", ", message.ToRecipients.Select(r => r.EmailAddress?.Address)),
                              message.Subject ?? string.Empty,
                              message.Body?.Content ?? string.Empty,
                              message.Id
                          )
                        );
  
      
                    logger.LogInformation("Marking message with ID {id} as read", message.Id);
                    await graphClient.Users[emailAddress].Messages[message.Id].PatchAsync(new Message
                    {
                        IsRead = true
                    });
                }
                return newEmails;

            }
            catch (Exception ex)
            {
                logger.LogError("An error occured why trying to read inbox");
                logger.LogError("Error Message : {message}", ex.Message);
                logger.LogError("Stack Trace: {stack}", ex.StackTrace);
                logger.LogError("Full Error: {ex}", ex);
                throw;
            }

        }
    }
}