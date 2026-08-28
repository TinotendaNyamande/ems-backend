using EMS.Application.Dtos.EmailAccounts;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccounts
{
    public record GetEmailAccountsQuery():IRequest<IEnumerable<EmailAccountDto>>
    {
      
    }
}
