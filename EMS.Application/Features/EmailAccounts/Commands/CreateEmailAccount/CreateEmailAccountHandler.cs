using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using EMS.Domain.Models;

namespace EMS.Application.Features.EmailAccounts.Commands.CreateEmailAccount
{
    public class CreateEmailAccountHandler(IEmailAccountRepository emailAccountRepository, IMapper mapper, IEncryptionService encryptionService) : ICommandHandler<CreateEmailAccountCommand, EmailAccountDto>
    {
        public async Task<EmailAccountDto> Handle(CreateEmailAccountCommand request, CancellationToken cancellationToken)
        {
            if (request.EmailType == Domain.Enums.EmailType.Office365)
            {
                if (string.IsNullOrWhiteSpace(request.ClientSecret))
                {
                    throw new ArgumentException("Client secret is required for Office365 email type.");
                }
                request = request with
                {
                    ClientSecret = encryptionService.EncryptData(request.ClientSecret)
                };
            }
            else
            {
                if(string.IsNullOrWhiteSpace(request.Password))
                {
                    throw new ArgumentException("Password is required for non-Office365 email types.");
                }
                request = request with
                {
                    Password = encryptionService.EncryptData(request.Password)
                };
            }


            var emailConfig = mapper.Map<EmailAccount>(request);

            await emailAccountRepository.CreateEmailAccountAsync(emailConfig);
            return mapper.Map<EmailAccountDto>(emailConfig);

        }
    }
}
