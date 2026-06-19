using AutoMapper;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.CreateEmailAccount
{
    public class CreateEmailAccountHandler(IEmailAccountRepository emailConfigurationRepository, IMapper mapper, IEncryptionService encryptionService) : IRequestHandler<CreateEmailAccountCommand, EmailAccountDto>
    {
        public async Task<EmailAccountDto> Handle(CreateEmailAccountCommand request, CancellationToken cancellationToken)
        {
            if (request.EmailType == Domain.Enums.EmailType.Office365)
            {
                request = request with
                {
                    ClientSecret = encryptionService.EncryptData(request.ClientSecret)
                };
            }
            else
            {
                request = request with
                {
                    Password = encryptionService.EncryptData(request.Password)
                };
            }


            var emailConfig = mapper.Map<EmailAccount>(request);

            await emailConfigurationRepository.CreateEmailAccountAsync(emailConfig);
            return mapper.Map<EmailAccountDto>(emailConfig);

        }
    }
}
