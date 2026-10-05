using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingAssignment
{
    public record GetEmailsPendingAssignmentQuery(Guid EmailAccountId):IQuery<IEnumerable<Email>>;
}