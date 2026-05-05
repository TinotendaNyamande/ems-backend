using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.CreateEmailConfig
{
    public class CreateEmailConfigHandler(IEmailConfigurationRepository emailConfigurationRepository,IMapper mapper) : IRequestHandler<CreateEmailConfigCommand,EmailConfigDto>
    {
        public async Task<EmailConfigDto> Handle(CreateEmailConfigCommand request, CancellationToken cancellationToken)
        {
            var emailConfig = mapper.Map<MailBoxConfig>(request);
            await emailConfigurationRepository.CreateEmailAccountAsync(emailConfig);
            return mapper.Map<EmailConfigDto>(emailConfig);

        }
    }
}
