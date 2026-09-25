using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.ChangeEmailAccountPassword
{
    public record ChangeEmailAccountPasswordCommand(Guid EmailId,string OldPassword, string NewPassword):ICommand
    {
   
    }
}
