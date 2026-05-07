using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.EmailId)
                .NotEmpty()
                .WithMessage("Email Id cannot be empty");
            RuleFor(x => x.NewPassword)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("New password cannot be empty");
            RuleFor(x => x.OldPassword)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Old password cannot be empty");
        }
    }
}
