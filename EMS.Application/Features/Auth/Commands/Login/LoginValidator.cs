using FluentValidation;

namespace EMS.Application.Features.Auth.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");
            RuleFor(x => x.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Password cannot be empty");
        }
    }
}
