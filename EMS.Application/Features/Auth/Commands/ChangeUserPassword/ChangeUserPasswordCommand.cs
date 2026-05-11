using MediatR;

namespace EMS.Application.Features.Auth.Commands.ChangeUserPassword
{
    public record ChangeUserPasswordCommand(string UserId,string Email, string Password, string NewPassword) : IRequest
    {
    }
}
