using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.Emails;
using EMS.Application.Features.EmailAttachments.Commands.CreateEmailAttachment;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.Emails.Commands.ReadEmailsFromInbox
{
    internal class ReadEmailsFromInboxHandler(IEmailRepository emailRepository, IEncryptionService encryptionService, IMediator mediator, ILogger<ReadEmailsFromInboxHandler> logger) : ICommandHandler<ReadEmailsFromInboxCommand>
    {
        public async Task Handle(ReadEmailsFromInboxCommand request, CancellationToken cancellationToken)
        {
            var messages = new List<IncomingEmailDto>();
            if (request.EmailType == EmailType.Gmail)
            {
                if (string.IsNullOrEmpty(request.Password))
                {
                    throw new InvalidOperationException("Password is required for Gmail email type");
                }
                var password = encryptionService.DecryptData(request.Password);
                var gmailMessages = await emailRepository.GetUnreadMessagesFromGmailInboxAsync(request.EmailAccountId, request.EmailAddress, password, cancellationToken);
                messages.AddRange(gmailMessages);
            }
            else if (request.EmailType == EmailType.Office365)
            {
                if (string.IsNullOrEmpty(request.ClientSecret))
                {
                    throw new InvalidOperationException("Client secret is required for Office365 email type");
                }
                var encryptedClientSecret = encryptionService.DecryptData(request.ClientSecret);
                if(string.IsNullOrEmpty(request.TenantId) || string.IsNullOrEmpty(request.ClientId))
                {
                    throw new InvalidOperationException("TenantId and ClientId are required for Office365 email type");
                }
                var office365Messages = await emailRepository.GetUnreadMessagesFromOffice365InboxAsync(request.EmailAccountId, request.TenantId, request.ClientId, encryptedClientSecret, request.EmailAddress, cancellationToken);
                messages.AddRange(office365Messages);
            }
            else
            {
                throw new InvalidOperationException($"Email type {request.EmailType} not supported");
            }
            foreach (var message in messages)
            {
                var isProcessed = await emailRepository.GetEmailByMessageIdAsync(message.ExternalMessageId);
                if (isProcessed == null)
                {
                  
                    var email = new Email(message.FromEmail, message.ToEmail, message.Subject, message.Body, request.EmailAccountId, message.ExternalMessageId);
                    foreach (var attachment in message.EmailAttachments)
                    {
                        await mediator.Send(new CreateEmailAttachmentCommand(email.Id, attachment.FileName, attachment.FilePath, attachment.FileType, attachment.FileSize), cancellationToken);
                    }

                    await emailRepository.CreateEmailAsync(email);
                }else
                {
                    logger.LogInformation("Email with messageId {messageId} has already been processed", message.ExternalMessageId);
                }
            }
        }
    }
}