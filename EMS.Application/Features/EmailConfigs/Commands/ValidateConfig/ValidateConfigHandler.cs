using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ValidateConfig
{
    public class ValidateConfigHandler(IEnumerable<IEmailProviderValidator> validators, IEmailConfigurationRepository emailConfigurationRepository, IEncryptionService encryptionService, IMapper mapper) : IRequestHandler<ValidateConfigCommand, bool>
    {


        public async Task<bool> Handle(ValidateConfigCommand request, CancellationToken cancellationToken)
        {

            var mailBoxConfig = await emailConfigurationRepository.GetEmailAccountAsync(request.Id);
            var validator = validators.FirstOrDefault(v => v.EmailType == mailBoxConfig.EmailType)
       ?? throw new InvalidOperationException($"No validator found for {mailBoxConfig.EmailType}");
            var mailBoxConfigDto = mapper.Map<ValidationAndTestEmailDto>(mailBoxConfig);
            if (mailBoxConfig.EmailType == Domain.Enums.EmailType.Office365)
            {
                mailBoxConfigDto.ClientSecret = encryptionService.DescryptData(mailBoxConfig.ClientSecret);
            }
            else
            {
                mailBoxConfigDto.Password = encryptionService.DescryptData(mailBoxConfig.Password);
            }
            var result = await validator.IsEmailConfigValidAsync(mailBoxConfigDto);
            if (result == true)
            {
                await emailConfigurationRepository.MarkAsValidated(mailBoxConfig.Id);
            }
            else
            {
                await emailConfigurationRepository.MarkAsInvalidated(mailBoxConfig.Id);
            }
            return result;


        }
    }
}
