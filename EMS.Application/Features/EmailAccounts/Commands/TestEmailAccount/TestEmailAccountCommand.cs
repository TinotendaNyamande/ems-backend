using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.TestEmailAccount
{
    public record TestEmailAccountCommand(Guid Id,string ToEmail) :IRequest
    {
    }
}
