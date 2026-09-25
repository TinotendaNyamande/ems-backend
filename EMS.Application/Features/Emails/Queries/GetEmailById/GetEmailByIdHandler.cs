using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailById
{
    internal class GetEmailByIdHandler(IEmailRepository emailRepository) : ICommandHandler<GetEmailByIdQuery, Email>
    {
        public Task<Email> Handle(GetEmailByIdQuery request, CancellationToken cancellationToken)
        {
            return emailRepository.GetEmailByIdAsync(request.Id);
        }
    }
}