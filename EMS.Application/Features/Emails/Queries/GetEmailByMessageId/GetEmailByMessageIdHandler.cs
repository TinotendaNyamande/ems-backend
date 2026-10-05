using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.Emails.Queries.GetEmailByMessageId
{
    internal class GetEmailByMessageIdHandler(IEmailRepository emailRepository) : IQueryHandler<GetEmailByMessageIdQuery, Email?>
    {
        public Task<Email?> Handle(GetEmailByMessageIdQuery request, CancellationToken cancellationToken)
        {
            return emailRepository.GetEmailByMessageIdAsync(request.MessageId)?? throw new ResourceNotFoundException("Email", request.MessageId);
        }
    }
}