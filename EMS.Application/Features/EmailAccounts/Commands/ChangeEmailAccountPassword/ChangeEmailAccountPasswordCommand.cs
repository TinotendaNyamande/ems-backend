using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.ChangeEmailAccountPassword
{
    public record ChangeEmailAccountPasswordCommand(Guid EmailId,string OldPassword, string NewPassword):IRequest
    {
   
    }
}
