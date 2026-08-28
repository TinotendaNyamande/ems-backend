using AutoMapper;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.TestEmailAccount
{
    internal class TestEmailHandler(IEmailSender emailSender,IEmailAccountRepository emailAccountRepository,IMapper mapper,IEncryptionService encryptionService):IRequestHandler<TestEmailAccountCommand>
    {
        public async Task Handle(TestEmailAccountCommand request, CancellationToken cancellationToken)
        {
            var emailAccount = await emailAccountRepository.GetEmailAccountAsync(request.Id);
            var emailAccountDto = mapper.Map<ValidationAndTestEmailAccountDto>(emailAccount);
            if(emailAccount.EmailType==Domain.Enums.EmailType.Office365)
            {
                emailAccountDto.ClientSecret = encryptionService.DecryptData(emailAccount.ClientSecret);
            }
            else
            {
                emailAccountDto.Password = encryptionService.DecryptData(emailAccount.Password);
            }
            await emailSender.SendTestEmailAsync(emailAccountDto, request.ToEmail);
        }
    }
}
