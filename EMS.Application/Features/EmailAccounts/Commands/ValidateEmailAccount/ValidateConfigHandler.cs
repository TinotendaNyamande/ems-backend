using AutoMapper;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.ValidateEmailAccount
{
    public class ValidateConfigHandler(IEnumerable<IEmailProviderValidator> validators, IEmailAccountRepository emailConfigurationRepository, IEncryptionService encryptionService, IMapper mapper) : IRequestHandler<ValidateAccountCommand, bool>
    {


        public async Task<bool> Handle(ValidateAccountCommand request, CancellationToken cancellationToken)
        {

            var emailAccount = await emailConfigurationRepository.GetEmailAccountAsync(request.Id);
            var validator = validators.FirstOrDefault(v => v.EmailType == emailAccount.EmailType)
       ?? throw new InvalidOperationException($"No validator found for {emailAccount.EmailType}");
            var emailAccountDto = mapper.Map<ValidationAndTestEmailAccountDto>(emailAccount);
            if (emailAccount.EmailType == Domain.Enums.EmailType.Office365)
            {
                emailAccountDto.ClientSecret = encryptionService.DecryptData(emailAccount.ClientSecret);
            }
            else
            {
                emailAccountDto.Password = encryptionService.DecryptData(emailAccount.Password);
            }
            var result = await validator.IsEmailConfigValidAsync(emailAccountDto);
            if (result == true)
            {
                await emailConfigurationRepository.MarkAsValidated(emailAccount.Id);
            }
            else
            {
                await emailConfigurationRepository.MarkAsInvalidated(emailAccount.Id);
            }
            return result;


        }
    }
}
