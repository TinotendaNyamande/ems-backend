using EMS.Application.Abstractions;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.EmailAccounts.Commands.DeleteEmailAccount
{
    public class DeleteEmailHandler(IEmailAccountRepository emailAccountRepository) : ICommandHandler<DeleteEmailAccountCommand>
    {
        public async Task Handle(DeleteEmailAccountCommand request, CancellationToken cancellationToken)
        {
            await emailAccountRepository.DeleteAsync(request.EmailId);
        }
    }
}
