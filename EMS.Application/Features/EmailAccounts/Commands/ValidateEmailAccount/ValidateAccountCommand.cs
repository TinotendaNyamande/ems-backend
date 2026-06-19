using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.ValidateEmailAccount
{
    public record ValidateAccountCommand(Guid Id) :IRequest<bool>
    {
    }
}
