using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.DeleteEmail
{
    public class DeleteEmailHandler(IEmailConfigurationRepository emailConfigurationRepository) : IRequestHandler<DeleteEmailCommand>
    {
        public async Task Handle(DeleteEmailCommand request, CancellationToken cancellationToken)
        {
            await emailConfigurationRepository.DeleteAsync(request.EmailId);
        }
    }
}
