using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingCategorization
{
    public class GetEmailsPendingCategorizationHandler(IEmailRepository emailRepository) : IQueryHandler<GetEmailsPendingCategorizationQuery,IEnumerable<Email>>
    {
        public async Task<IEnumerable<Email>> Handle(GetEmailsPendingCategorizationQuery request, CancellationToken cancellationToken)
        {
            return await emailRepository.GetEmailsPendingCategorizationAsync();
        }
    }
}