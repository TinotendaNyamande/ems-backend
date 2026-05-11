using FluentValidation;

namespace EMS.Application.Features.Auth.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordValidator:AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor(x => x.Email)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");
            RuleFor(x => x.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Password cannot be empty");
            RuleFor(x => x.NewPassword)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("NewPassword cannot be empty");
            RuleFor(x=>x.UserId)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("UserId cannot be empty");
        }
    }
}
