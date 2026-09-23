using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetAllValidatedEmailAccounts
{
    public record GetAllValidatedEmailAccountsQuery:IRequest<IEnumerable<EmailAccount>>;
}