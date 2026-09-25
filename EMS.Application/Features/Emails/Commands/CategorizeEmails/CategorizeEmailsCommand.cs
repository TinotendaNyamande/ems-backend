using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.CategorizeEmails
{
    public record CategorizeEmailsCommand(Guid EmailId,Guid EmailAccountId,string Subject,string Body):ICommand;
}