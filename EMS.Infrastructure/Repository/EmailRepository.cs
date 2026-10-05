using Azure.Identity;
using EMS.Application.Dtos.Emails;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using MimeKit;
using EMS.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailRepository(ApplicationDbContext context, ILogger<EmailRepository> logger, IConfiguration configuration) : IEmailRepository
    {
        public async Task AssignEmailCategoryAsync(Guid id, Guid newCategoryId)
        {
            var email = await context.Emails.FindAsync(id) ?? throw new ResourceNotFoundException("Email", id);
            email.ChangeStatus(EmailStatus.Categorized);
            email.ChangeCategory(newCategoryId);
        }

        public async Task ChangeEmailCategoryAsync(Guid id, Guid newCategoryId)
        {
            var emailCategory = await context.Emails.FindAsync(id)
            ?? throw new ResourceNotFoundException("Email category", id);
            emailCategory.ChangeCategory(newCategoryId);
        }

        public async Task ChangeEmailStatusAsync(Guid id, EmailStatus newStatus)
        {
            var email = await context.Emails.FindAsync(id) ?? throw new ResourceNotFoundException("Email", id);
            email.ChangeStatus(newStatus);
        }

        public async Task<Email> CreateEmailAsync(Email email)
        {
            context.Add(email);
            return email;
        }

        public async Task DeleteEmailAsync(Guid id)
        {
            var email = await context.Emails.FindAsync(id) ?? throw new ResourceNotFoundException("Email", id);
            context.Remove(email);
        }

        public async Task MarkEmailAsAssignedAsync(Guid emailId)
        {
            var email = await context.Emails.FindAsync(emailId) ?? throw new ResourceNotFoundException("Email", emailId);
            email.NewEmailAssigned();
        }

        public async Task<Email?> GetEmailByIdAsync(Guid id)
        {
            return await context.Emails.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync() ;
        }

        public async Task<Email?> GetEmailByMessageIdAsync(string messageId)
        {
            return await context.Emails.Where(e => e.ExternalMessageId == messageId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Email>> GetEmailsByCategoryIdAsync(Guid categoryId)
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

        public async Task<IEnumerable<IncomingEmailDto>> GetUnreadMessagesFromGmailInboxAsync(
            Guid id,
            string emailAddress,
            string password,
            CancellationToken cancellationToken)
        {
            var newEmails = new List<IncomingEmailDto>();
            var attachmentBasePath = configuration["EmailAttachmentsPath"];
            if (string.IsNullOrEmpty(attachmentBasePath))
            {
                throw new InvalidOperationException("EmailAttachmentsPath is not configured.");
            }

            try
            {
                using var client = new ImapClient();

                logger.LogInformation("Attempting to connect to client");
                await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect, cancellationToken);
                logger.LogInformation("Connected to client successfully");

                logger.LogInformation("Attempting to authenticate");
                await client.AuthenticateAsync(emailAddress, password, cancellationToken);
                logger.LogInformation("Successfully connected to Gmail account for account id {id}", id);

                var inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadWrite, cancellationToken);

                var unreadUids = await inbox.SearchAsync(MailKit.Search.SearchQuery.NotSeen, cancellationToken);
                logger.LogInformation("Found {count} unread messages for account id {id}", unreadUids.Count, id);

                var accountAttachmentRoot = Path.Combine(
                    attachmentBasePath,
                    id.ToString());

                foreach (var uid in unreadUids)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    logger.LogInformation("Start: processing message with uid {uid}", uid);

                    var message = await inbox.GetMessageAsync(uid, cancellationToken);

                    var attachments = new List<EmailAttachmentDto>();

                    if (message.Attachments.Any())
                    {
                        var messageFolder = Path.Combine(
                            accountAttachmentRoot,
                            SanitizeFileName(message.MessageId ?? uid.ToString()));

                        Directory.CreateDirectory(messageFolder);

                        foreach (var attachment in message.Attachments)
                        {
                            try
                            {
                                var savedPath = await SaveAttachmentAsync(
                                    attachment,
                                    messageFolder,
                                    cancellationToken);

                                attachments.Add(new EmailAttachmentDto(
                                    FileName: attachment.ContentDisposition?.FileName
                                              ?? attachment.ContentType?.Name
                                              ?? "attachment",
                                    FileType: attachment.ContentType?.MimeType ?? "application/octet-stream",
                                    FileSize: attachment.ContentDisposition?.Size
                                          ?? (attachment is MimePart mp ? mp.Content!.Stream!.Length : 0),
                                    FilePath: savedPath));

                                logger.LogInformation("Saved attachment {file} to {path}",
                                    attachment.ContentDisposition?.FileName, savedPath);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "Failed to save attachment from message {msgId}", message.MessageId);
                            }
                        }
                    }

                    newEmails.Add(new IncomingEmailDto(
                        message.From.ToString(),
                        message.To.ToString(),
                        message.Subject ?? string.Empty,
                        message.TextBody ?? string.Empty,
                        message.MessageId!,
                        attachments));

                    logger.LogInformation("Marking message with ID {id} as read", message.MessageId);
                    await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true, cancellationToken);
                }

                await client.DisconnectAsync(true, cancellationToken);
                logger.LogInformation("Disconnected account with Id {id} after finishing reading messages", id);

                return newEmails;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while trying to read inbox for account {id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<IncomingEmailDto>> GetUnreadMessagesFromOffice365InboxAsync(Guid id, string tenantId, string clientId, string clientSecret, string emailAddress, CancellationToken cancellationToken)
        {
            try
            {
                var newEmails = new List<IncomingEmailDto>();
                var attachments = new List<EmailAttachmentDto>();

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

                foreach (var message in messages!.Value!)
                {
                    logger.LogInformation("Start: Reading individual messages for Office 365 account {id}", id);
                    logger.LogInformation("Start: processing message with message Id {id}", message.Id);

                    logger.LogInformation("Message with ID {id} has not been processed before", message.Id);
                    var emailId = Guid.NewGuid();
                    newEmails.Add(new IncomingEmailDto
                      (
                         message.From?.EmailAddress?.Address ?? string.Empty,
                         string.Join(", ", message.ToRecipients!.Select(r => r.EmailAddress?.Address)),
                          message.Subject ?? string.Empty,
                          message.Body?.Content ?? string.Empty,
                          message.Id!,
                          attachments
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
        private async Task<string> SaveAttachmentAsync(
    MimeEntity attachment,
    string targetDirectory,
    CancellationToken cancellationToken)
        {
            // Resolve a safe file name
            var rawName = attachment.ContentDisposition?.FileName
                          ?? attachment.ContentType?.Name
                          ?? $"attachment_{Guid.NewGuid():N}";

            var fileName = SanitizeFileName(rawName);
            var fullPath = Path.Combine(targetDirectory, fileName);

            // Ensure unique path if a file with the same name already exists
            fullPath = EnsureUniquePath(fullPath);

            await using var stream = File.Create(fullPath);

            switch (attachment)
            {
                case MessagePart messagePart:   
                    await messagePart!.Message!.WriteToAsync(stream, cancellationToken);
                    break;

                case MimePart mimePart:
                    await mimePart!.Content!.DecodeToAsync(stream, cancellationToken);
                    break;

                default:
                    throw new NotSupportedException(
                        $"Unsupported attachment type: {attachment.GetType().Name}");
            }

            return fullPath;
        }

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return $"attachment_{Guid.NewGuid():N}";

            var invalid = Path.GetInvalidFileNameChars();
            var cleaned = new string(name.Select(c => invalid.Contains(c) ? '_' : c).ToArray());

            cleaned = Path.GetFileName(cleaned);

            const int maxLen = 150;
            if (cleaned.Length > maxLen)
            {
                var ext = Path.GetExtension(cleaned);
                cleaned = cleaned[..(maxLen - ext.Length)] + ext;
            }

            return cleaned;
        }

        private static string EnsureUniquePath(string fullPath)
        {
            if (!File.Exists(fullPath)) return fullPath;

            var dir = Path.GetDirectoryName(fullPath)!;
            var name = Path.GetFileNameWithoutExtension(fullPath);
            var ext = Path.GetExtension(fullPath);

            for (int i = 1; i < int.MaxValue; i++)
            {
                var candidate = Path.Combine(dir, $"{name}_{i}{ext}");
                if (!File.Exists(candidate)) return candidate;
            }

            throw new IOException($"Could not find a unique filename for {fullPath}");
        }
    }
}