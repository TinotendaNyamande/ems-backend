using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailByMessageId
{
    internal class GetEmailByMessageIdHandler(IEmailRepository emailRepository) : ICommandHandler<GetEmailByMessageIdQuery, Email>
    {
        public Task<Email> Handle(GetEmailByMessageIdQuery request, CancellationToken cancellationToken)
        {
            return emailRepository.GetEmailByMessageIdAsync(request.MessageId);
        }
    }
}