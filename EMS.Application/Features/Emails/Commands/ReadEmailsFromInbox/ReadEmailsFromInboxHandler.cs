using AutoMapper;
using EMS.Application.Dtos.Emails;
using EMS.Application.Features.Emails.Commands.CreateEmail;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.ReadEmailsFromInbox
{
    internal class ReadEmailsFromInboxHandler(IEmailRepository emailRepository, IEncryptionService encryptionService,IMapper mapper) : IRequestHandler<ReadEmailsFromInboxCommand>
    {
        public async Task Handle(ReadEmailsFromInboxCommand request, CancellationToken cancellationToken)
        {
            var messages = new List<IncomingEmailDto>();
            if (request.EmailType == EmailType.Gmail)
            {
                var password = encryptionService.DecryptData(request.Password);
                var gmailMessages = await emailRepository.GetUnreadMessagesFromGmailInboxAsync(request.Id, request.EmailAddress, password, cancellationToken);
                messages.AddRange(gmailMessages);
            }
            else if (request.EmailType == EmailType.Office365)
            {
                var encryptedClientSecret = encryptionService.DecryptData(request.ClientSecret);
                var office365Messages = await emailRepository.GetUnreadMessagesFromOffice365InboxAsync(request.Id, request.TenantId, request.ClientId, encryptedClientSecret, request.EmailAddress, cancellationToken);
                messages.AddRange(office365Messages);
            }
            else
            {
                throw new InvalidOperationException($"Email type {request.EmailType} not supported");
            }
            foreach(var message in messages)
            {
                var isProcessed = await emailRepository.GetEmailByMessageIdAsync(message.ExternalMessageId);
                if (isProcessed==null)
                {
                    await emailRepository.CreateEmailAsync(mapper.Map<Email>(message));
                }
            }
        }
    }
}