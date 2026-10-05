using EMS.Application.Abstractions;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.EmailAccounts.Commands.ChangeClientSecret
{
    public class ChangeClientSecretHandler(IEmailAccountRepository emailAccountRepository,IEncryptionService encryptionService) : ICommandHandler<ChangeClientSecretCommand>
    {
        public async Task Handle(ChangeClientSecretCommand request, CancellationToken cancellationToken)
        {
            var newSecret = encryptionService.EncryptData(request.NewSecret);
            await emailAccountRepository.ChangeApplicationSecretAsync(request.EmailId,request.OldSecret, newSecret);
        }
    }
}
