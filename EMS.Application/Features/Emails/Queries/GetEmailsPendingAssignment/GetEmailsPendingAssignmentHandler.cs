using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingAssignment
{
    internal class GetEmailsPendingAssignmentHandler(IEmailRepository emailRepository) : IRequestHandler<GetEmailsPendingAssignmentQuery, IEnumerable<Email>>
    {
        public async Task<IEnumerable<Email>> Handle(GetEmailsPendingAssignmentQuery request, CancellationToken cancellationToken)
        {
            return await emailRepository.GetEmailsPendingAssignmentAsync(request.EmailAccountId);
        }
    }
}
