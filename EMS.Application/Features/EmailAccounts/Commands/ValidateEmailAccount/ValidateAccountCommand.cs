using EMS.Application.Abstractions;
using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.ValidateEmailAccount
{
    public record ValidateAccountCommand(Guid EmailAccountId) :ICommand<bool>
    {
    }
}
