using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailsByCategoryId
{
    internal class GetEmailsByCategoryIdHandler(IEmailRepository emailRepository) : ICommandHandler<GetEmailsByCategoryIdQuery, IEnumerable<Email>>
    {
        public async Task<IEnumerable<Email>> Handle(GetEmailsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            return await emailRepository.GetEmailsByCategoryIdAsync(request.CategoryId);
        }
    }
}