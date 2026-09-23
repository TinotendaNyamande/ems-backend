using EMS.Application.Interfaces;
using EMS.Domain.Models;
using Azure.Identity;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using EMS.Domain.Enums;
using EMS.Infrastructure.RabbitMQServices;
using EMS.Contracts.Events.Email;
using EMS.Infrastructure.RabbitMQServices.Constants;
using MediatR;
using EMS.Application.Features.Emails.Commands.CreateEmail;
using EMS.Application.Features.EmailAccounts.Queries.GetAllValidatedEmailAccounts;
using EMS.Application.Features.Emails.Queries.GetEmailByMessageId;
using EMS.Application.Features.Emails.Commands.ReadEmailsFromInbox;

namespace EMS.EmailReader.Worker.Services
{
    internal class EmailReaderProcessor(IMediator mediator,ILogger<EmailReaderProcessor> logger) : IEmailReaderProcessor
    {
        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Start: Read from email service");
            var emailAccounts = await mediator.Send(new GetAllValidatedEmailAccountsQuery(), cancellationToken);
            logger.LogInformation("Found {count} number of validated mailboxes", emailAccounts.Count());
            if (emailAccounts.Any())
            {
                foreach (var emailAccount in emailAccounts)
                {
                    logger.LogInformation("Processing mailbox {id} of type {type}", emailAccount.Id, emailAccount.EmailType);

                    await mediator.Send(new ReadEmailsFromInboxCommand(emailAccount.Id, emailAccount.EmailAddress, emailAccount.EmailType, emailAccount.Password, emailAccount.ClientSecret,emailAccount.TenantId,emailAccount.ClientId), cancellationToken);


                }
            }

        }

    }
}