using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangeEmailPassword
{
    public class ChangeEmailPasswordHandler(IEmailConfigurationRepository emailConfigurationRepository,IMapper mapper,IEncryptionService encryptionService) : IRequestHandler<ChangeEmailPasswordCommand>
    {
        public async Task Handle(ChangeEmailPasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordDto = mapper.Map<ChangeEmailPasswordDto>(request);
            changePasswordDto.NewPassword = encryptionService.EncryptData(changePasswordDto.NewPassword);
            await emailConfigurationRepository.ChangePasswordAsync(request.EmailId,changePasswordDto);
        }
    }
}
