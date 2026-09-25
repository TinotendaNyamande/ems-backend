using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.Emails.Queries.GetEmailsByCategoryId
{
    public record GetEmailsByCategoryIdQuery(Guid CategoryId):ICommand<IEnumerable<Email>>;
}