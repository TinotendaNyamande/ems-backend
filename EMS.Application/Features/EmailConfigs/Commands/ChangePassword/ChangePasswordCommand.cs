using MediatR;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangePassword
{
    public record ChangePasswordCommand(Guid emailId,string oldPassword, string newPassword):IRequest
    {
        public Guid EmailId { get; init; }=emailId;
        public string OldPassword { get; init; }=oldPassword;
        public string NewPassword { get; init; } = newPassword;
    }
}
