using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailByMessageId
{
    public record GetEmailByMessageIdQuery(string MessageId):IRequest<Email>;
}