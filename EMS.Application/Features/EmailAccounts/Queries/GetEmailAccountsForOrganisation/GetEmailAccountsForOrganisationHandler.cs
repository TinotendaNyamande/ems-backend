using AutoMapper;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountsForOrganisation
{
    public class GetEmailAccountsForOrganisationHandler(IEmailAccountRepository emailAccountRepository,IMapper mapper) : IRequestHandler<GetEmailAccountsForOrganisationQuery, IEnumerable<EmailAccountDto>>
    {
        public async Task<IEnumerable<EmailAccountDto>> Handle(GetEmailAccountsForOrganisationQuery request, CancellationToken cancellationToken)
        {
            var emailAccounts = await emailAccountRepository.GetEmailAccountsForOrganisationAsync(request.OrganisationId);
            return mapper.Map<IEnumerable<EmailAccountDto>>(emailAccounts);
        }
    }
}
