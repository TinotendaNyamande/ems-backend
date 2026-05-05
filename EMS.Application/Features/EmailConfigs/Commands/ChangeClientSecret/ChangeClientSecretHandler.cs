using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangeClientSecret
{
    public class ChangeClientSecretHandler(IEmailConfigurationRepository emailConfigurationRepository,IMapper mapper) : IRequestHandler<ChangeClientSecretCommand>
    {
        public async Task Handle(ChangeClientSecretCommand request, CancellationToken cancellationToken)
        {
            var changeSecretDto = mapper.Map<ChangeClientSecretDto>(request);
            await emailConfigurationRepository.ChangeApplicationSecretAsync(request.EmailId, changeSecretDto);
        }
    }
}
