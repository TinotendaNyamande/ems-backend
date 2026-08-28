using AutoMapper;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccounts
{
    public class GetEmailAccountsHandler(IEmailAccountRepository emailAccountRepository,IMapper mapper) : IRequestHandler<GetEmailAccountsQuery, IEnumerable<EmailAccountDto>>
    {
        public async Task<IEnumerable<EmailAccountDto>> Handle(GetEmailAccountsQuery request, CancellationToken cancellationToken)
        {
            var emailAccounts = await emailAccountRepository.GetEmailAccountsAsync();
            return mapper.Map<IEnumerable<EmailAccountDto>>(emailAccounts);
        }
    }
}
