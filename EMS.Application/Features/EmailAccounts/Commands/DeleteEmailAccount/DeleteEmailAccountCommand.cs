using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.DeleteEmailAccount
{
    public class DeleteEmailAccountCommand(Guid emailId) : IRequest
    {
        public Guid EmailId { get; init; } = emailId;
    }
}
