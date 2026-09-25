using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.ChangeClientSecret
{
    public class ChangeClientSecretHandler(IEmailAccountRepository emailConfigurationRepository,IMapper mapper,IEncryptionService encryptionService) : ICommandHandler<ChangeClientSecretCommand>
    {
        public async Task Handle(ChangeClientSecretCommand request, CancellationToken cancellationToken)
        {
            var changeSecretDto = mapper.Map<ChangeClientSecretDto>(request);
            changeSecretDto.NewSecret = encryptionService.EncryptData(changeSecretDto.NewSecret);
            await emailConfigurationRepository.ChangeApplicationSecretAsync(request.EmailId, changeSecretDto);
        }
    }
}
