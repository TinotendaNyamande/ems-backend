using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ValidateConfig
{
    public class ValidateConfigHandler(IEnumerable<IEmailProviderValidator> validators,IEmailConfigurationRepository emailConfigurationRepository) :IRequestHandler<ValidateConfigCommand,bool>
    {
 

        public async Task<bool> Handle(ValidateConfigCommand request, CancellationToken cancellationToken)
        {
   
            var mailBoxConfig = await emailConfigurationRepository.GetEmailAccountAsync(request.Id);
            var validator = validators.FirstOrDefault(v => v.EmailType == mailBoxConfig.EmailType)
       ?? throw new InvalidOperationException($"No validator found for {mailBoxConfig.EmailType}");
            
            var result =  await validator.IsEmailConfigValidAsync(mailBoxConfig);
            if (result == true)
            {
                await emailConfigurationRepository.MarkAsValidated(mailBoxConfig.Id);
            }else
            {
                await emailConfigurationRepository.MarkAsInvalidated(mailBoxConfig.Id);
            }
            return result;

           
        }
    }
}
