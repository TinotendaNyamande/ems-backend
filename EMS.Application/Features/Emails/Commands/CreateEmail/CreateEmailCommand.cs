using EMS.Application.Abstractions;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.CreateEmail
{
    public record CreateEmailCommand(string FromEmail, string ToEmail, string Subject, string Body, Guid EmailAccountId,string ExternalMessageId):ICommand<Email>;
}