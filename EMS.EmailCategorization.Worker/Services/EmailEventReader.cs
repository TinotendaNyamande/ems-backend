using EMS.Application.Features.Emails.Queries.GetEmailsPendingCategorization;
using EMS.Application.Interfaces;
using EMS.Contracts.Events.Email;
using MediatR;

namespace EMS.EmailCategorization.Worker.Services
{
    internal class EmailEventReader(IMediator mediator,ICategorizeEmails categorizeEmails):IEmailEventReader
    {

        public async Task ReadEmailsPendingCategorizationAsync(CancellationToken cancellationToken)
        {
            var emails = await mediator.Send(new GetEmailsPendingCategorizationQuery(),cancellationToken);
            foreach(var email in emails)
            {
                var emailEvent = new EmailReceivedEvent
                {
                    EmailId = email.Id,
                    Subject= email.Subject,
                    Body= email.Body,
                    EmailAccountId= email.EmailAccountId,
                    Sender = email.FromEmail,
                    ReceivedAt = email.CreatedAt
                };
                await categorizeEmails.ProcessAsync(emailEvent, new CancellationToken());
            }
        }
    }
}