using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailAccounts;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccounts
{
    public record GetEmailAccountsQuery():ICommand<IEnumerable<EmailAccountDto>>
    {
      
    }
}
