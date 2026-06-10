using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangeClientSecret
{
    public class ChangeClientSecretHandler(IEmailConfigurationRepository emailConfigurationRepository,IMapper mapper,IEncryptionService encryptionService) : IRequestHandler<ChangeClientSecretCommand>
    {
        public async Task Handle(ChangeClientSecretCommand request, CancellationToken cancellationToken)
        {
            var changeSecretDto = mapper.Map<ChangeClientSecretDto>(request);
            changeSecretDto.NewSecret = encryptionService.EncryptData(changeSecretDto.NewSecret);
            await emailConfigurationRepository.ChangeApplicationSecretAsync(request.EmailId, changeSecretDto);
        }
    }
}
