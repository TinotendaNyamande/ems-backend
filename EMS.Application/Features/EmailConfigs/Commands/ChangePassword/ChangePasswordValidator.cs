using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangePassword
{
    public class ChangePasswordValidator:AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x=>x.EmailId).NotEmpty().WithMessage("Email Id cannot be empty");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New password cannot be empty");
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Old password cannot be empty");
        }
    }
}
