using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangeEmailPassword
{
    public record ChangeEmailPasswordCommand(Guid EmailId,string OldPassword, string NewPassword):IRequest
    {
   
    }
}
