using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetAllValidatedEmailAccounts
{
    internal class GetAllValidatedEmailAccountsHandler(IEmailAccountRepository emailAccountRepository) : IRequestHandler<GetAllValidatedEmailAccountsQuery, IEnumerable<EmailAccount>>
    {
        public async Task<IEnumerable<EmailAccount>> Handle(GetAllValidatedEmailAccountsQuery request, CancellationToken cancellationToken)
        {
            return await emailAccountRepository.GetAllValidatedEmailAccountsAsync();
        }
    }
}
