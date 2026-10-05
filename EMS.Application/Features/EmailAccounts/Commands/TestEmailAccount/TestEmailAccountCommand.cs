using EMS.Application.Abstractions;

namespace EMS.Application.Features.EmailAccounts.Commands.TestEmailAccount
{
    public record TestEmailAccountCommand(Guid EmailAccountId,string ToEmail) :ICommand
    {
    }
}
