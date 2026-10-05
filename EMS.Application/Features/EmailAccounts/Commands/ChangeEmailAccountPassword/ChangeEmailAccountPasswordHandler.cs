using EMS.Application.Abstractions;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.EmailAccounts.Commands.ChangeEmailAccountPassword
{
    public class ChangeEmailPasswordHandler(IEmailAccountRepository emailAccountRepository,IEncryptionService encryptionService) : ICommandHandler<ChangeEmailAccountPasswordCommand>
    {
        public async Task Handle(ChangeEmailAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            var newPassword = encryptionService.EncryptData(request.NewPassword);
            await emailAccountRepository.ChangePasswordAsync(request.EmailId, request.OldPassword, newPassword);
        }
    }
}
