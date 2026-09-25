using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingCategorization
{
    public record GetEmailsPendingCategorizationQuery():ICommand<IEnumerable<Email>>;
}