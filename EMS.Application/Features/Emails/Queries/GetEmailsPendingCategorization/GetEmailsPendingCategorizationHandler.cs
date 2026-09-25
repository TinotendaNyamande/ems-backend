using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingCategorization
{
    public class GetEmailsPendingCategorizationHandler(IEmailRepository emailRepository) : ICommandHandler<GetEmailsPendingCategorizationQuery,IEnumerable<Email>>
    {
        public async Task<IEnumerable<Email>> Handle(GetEmailsPendingCategorizationQuery request, CancellationToken cancellationToken)
        {
            return await emailRepository.GetEmailsPendingCategorizationAsync();
        }
    }
}