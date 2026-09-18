using EMS.Application.Interfaces;
using EMS.Contracts.Events.Email;

namespace EMS.EmailCategorization.Worker.Services
{
    internal class EmailEventReader(IEmailRepository emailRepository,ICategorizeEmails categorizeEmails):IEmailEventReader
    {

        public async Task ReadEmailsPendingCategorizationAsync(CancellationToken cancellationToken)
        {
            var emails = await emailRepository.GetEmailsPendingCategorization();
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