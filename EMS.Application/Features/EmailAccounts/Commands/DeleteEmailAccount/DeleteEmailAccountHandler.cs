using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.DeleteEmailAccount
{
    public class DeleteEmailHandler(IEmailAccountRepository emailConfigurationRepository) : IRequestHandler<DeleteEmailAccountCommand>
    {
        public async Task Handle(DeleteEmailAccountCommand request, CancellationToken cancellationToken)
        {
            await emailConfigurationRepository.DeleteAsync(request.EmailId);
        }
    }
}
