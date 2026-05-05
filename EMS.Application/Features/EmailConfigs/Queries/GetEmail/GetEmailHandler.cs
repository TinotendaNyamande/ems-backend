using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Queries.GetEmail
{
    public class GetEmailHandler(IEmailConfigurationRepository emailConfigurationRepository,IMapper mapper) : IRequestHandler<GetEmailQuery, EmailConfigDto>
    {
        public async Task<EmailConfigDto> Handle(GetEmailQuery request, CancellationToken cancellationToken)
        {
            var emailConfig = await emailConfigurationRepository.GetEmailAccountAsync(request.Id);
            return mapper.Map<EmailConfigDto>(emailConfig);
        }
    }
}
