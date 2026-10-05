using EMS.Application.Abstractions;

namespace EMS.Application.Features.EmailAccounts.Commands.ValidateEmailAccount
{
    public record ValidateAccountCommand(Guid EmailAccountId) :ICommand<bool>
    {
    }
}
