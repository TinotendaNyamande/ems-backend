using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingAssignment
{
    public record GetEmailsPendingAssignmentQuery(Guid EmailAccountId):IRequest<IEnumerable<Email>>;
}