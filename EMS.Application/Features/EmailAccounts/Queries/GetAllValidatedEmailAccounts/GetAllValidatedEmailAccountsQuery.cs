using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.EmailAccounts.Queries.GetAllValidatedEmailAccounts
{
    public record GetAllValidatedEmailAccountsQuery:IQuery<IEnumerable<EmailAccount>>;
}