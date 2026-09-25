using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.DeleteEmailAccount
{
    public class DeleteEmailAccountCommand(Guid emailId) : ICommand
    {
        public Guid EmailId { get; init; } = emailId;
    }
}
