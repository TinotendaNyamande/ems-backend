using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.ChangeClientSecret
{
    public record ChangeClientSecretCommand(Guid EmailId,string OldSecret,string NewSecret):ICommand
    {

    }
}
