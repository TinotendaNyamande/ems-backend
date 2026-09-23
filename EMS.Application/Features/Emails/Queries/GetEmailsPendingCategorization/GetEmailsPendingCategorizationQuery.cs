using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingCategorization
{
    public record GetEmailsPendingCategorizationQuery():IRequest<IEnumerable<Email>>;
}