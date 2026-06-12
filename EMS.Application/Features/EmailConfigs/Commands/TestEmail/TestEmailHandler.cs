using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.TestEmail
{
    internal class TestEmailHandler(IEmailSender emailSender,IEmailConfigurationRepository emailConfigurationRepository,IMapper mapper,IEncryptionService encryptionService):IRequestHandler<TestEmailCommand>
    {
        public async Task Handle(TestEmailCommand request, CancellationToken cancellationToken)
        {
            var mailBoxConfig = await emailConfigurationRepository.GetEmailAccountAsync(request.Id);
            var mailBoxConfigDto = mapper.Map<ValidationAndTestEmailDto>(mailBoxConfig);
            if(mailBoxConfig.EmailType==Domain.Enums.EmailType.Office365)
            {
                mailBoxConfigDto.ClientSecret = encryptionService.DescryptData(mailBoxConfig.ClientSecret);
            }
            else
            {
                mailBoxConfigDto.Password = encryptionService.DescryptData(mailBoxConfig.Password);
            }
            await emailSender.SendTestEmailAsync(mailBoxConfigDto, request.ToEmail);
        }
    }
}
