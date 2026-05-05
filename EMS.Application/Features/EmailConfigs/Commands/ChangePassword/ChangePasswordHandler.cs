using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangePassword
{
    public class ChangePasswordHandler(IEmailConfigurationRepository emailConfigurationRepository,IMapper mapper) : IRequestHandler<ChangePasswordCommand>
    {
        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordDto = mapper.Map<ChangePasswordDto>(request);
            await emailConfigurationRepository.ChangePasswordAsync(request.EmailId,changePasswordDto);
        }
    }
}
