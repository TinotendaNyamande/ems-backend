using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.TestEmail
{
    internal class TestEmailHandler(IEmailSender emailSender,IEmailConfigurationRepository emailConfigurationRepository):IRequestHandler<TestEmailCommand>
    {
        public async Task Handle(TestEmailCommand request, CancellationToken cancellationToken)
        {
            var mailBoxConfig = await emailConfigurationRepository.GetEmailAccountAsync(request.Id);
            await emailSender.SendTestEmailAsync(mailBoxConfig, request.ToEmail);
        }
    }
}
