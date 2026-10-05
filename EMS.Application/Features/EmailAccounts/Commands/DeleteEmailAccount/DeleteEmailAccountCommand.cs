using EMS.Application.Abstractions;

namespace EMS.Application.Features.EmailAccounts.Commands.DeleteEmailAccount
{
    public class DeleteEmailAccountCommand(Guid emailId) : ICommand
    {
        public Guid EmailId { get; init; } = emailId;
    }
}
