using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingAssignment
{
    internal class GetEmailsPendingAssignmentHandler(IEmailRepository emailRepository) : ICommandHandler<GetEmailsPendingAssignmentQuery, IEnumerable<Email>>
    {
        public async Task<IEnumerable<Email>> Handle(GetEmailsPendingAssignmentQuery request, CancellationToken cancellationToken)
        {
            return await emailRepository.GetEmailsPendingAssignmentAsync(request.EmailAccountId);
        }
    }
}
