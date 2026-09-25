using EMS.Application.Abstractions;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingAssignment
{
    public record GetEmailsPendingAssignmentQuery(Guid EmailAccountId):ICommand<IEnumerable<Email>>;
}