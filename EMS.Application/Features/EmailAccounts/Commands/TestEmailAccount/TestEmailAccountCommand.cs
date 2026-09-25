using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.TestEmailAccount
{
    public record TestEmailAccountCommand(Guid EmailAccountId,string ToEmail) :ICommand
    {
    }
}
