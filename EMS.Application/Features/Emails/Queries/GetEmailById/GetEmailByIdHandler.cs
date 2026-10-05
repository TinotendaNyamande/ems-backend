using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.Emails.Queries.GetEmailById
{
    internal class GetEmailByIdHandler(IEmailRepository emailRepository) : IQueryHandler<GetEmailByIdQuery, Email?>
    {
        public Task<Email?> Handle(GetEmailByIdQuery request, CancellationToken cancellationToken)
        {
            return emailRepository.GetEmailByIdAsync(request.Id)?? throw new ResourceNotFoundException("Email", request.Id);
        }
    }
}