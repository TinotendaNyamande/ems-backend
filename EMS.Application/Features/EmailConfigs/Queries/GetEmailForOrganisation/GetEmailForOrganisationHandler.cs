using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Queries.GetEmailForOrganisation
{
    public class GetEmailForOrganisationHandler(IEmailConfigurationRepository emailConfigurationRepository,IMapper mapper) : IRequestHandler<GetEmailForOrganisationQuery, IEnumerable<EmailConfigDto>>
    {
        public async Task<IEnumerable<EmailConfigDto>> Handle(GetEmailForOrganisationQuery request, CancellationToken cancellationToken)
        {
            var emailConfigs = await emailConfigurationRepository.GetEmailAccountsForOrganisationAsync(request.OrganisationId);
            return mapper.Map<IEnumerable<EmailConfigDto>>(emailConfigs);
        }
    }
}
