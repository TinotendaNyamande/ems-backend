using EMS.Application.Features.Emails.Commands.CategorizeEmails;
using EMS.Application.Features.Emails.Queries.GetEmailsPendingCategorization;
using EMS.Application.Interfaces;
using EMS.Contracts.Events.Email;
using MediatR;

namespace EMS.EmailCategorization.Worker.Services
{
    internal class EmailCategorizerProcess(IMediator mediator):IEmailCategorizerProcess
    {

        public async Task ReadEmailsPendingCategorizationAsync(CancellationToken cancellationToken)
        {
            var emails = await mediator.Send(new GetEmailsPendingCategorizationQuery(),cancellationToken);
            foreach(var email in emails)
            {
                await mediator.Send(new CategorizeEmailsCommand(email.Id,email.EmailAccountId,email.Subject,email.Body),cancellationToken);
            }
        }
    }
}