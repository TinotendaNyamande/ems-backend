using AutoMapper;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.ChangeEmailAccountPassword
{
    public class ChangeEmailPasswordHandler(IEmailAccountRepository emailConfigurationRepository,IMapper mapper,IEncryptionService encryptionService) : IRequestHandler<ChangeEmailAccountPasswordCommand>
    {
        public async Task Handle(ChangeEmailAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordDto = mapper.Map<ChangeEmailPasswordDto>(request);
            changePasswordDto.NewPassword = encryptionService.EncryptData(changePasswordDto.NewPassword);
            await emailConfigurationRepository.ChangePasswordAsync(request.EmailId,changePasswordDto);
        }
    }
}
