using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangePassword
{
    public record ChangePasswordCommand(Guid EmailId,string OldPassword, string NewPassword):IRequest
    {
   
    }
}
