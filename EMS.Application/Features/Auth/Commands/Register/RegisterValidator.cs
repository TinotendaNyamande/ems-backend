using FluentValidation;

namespace EMS.Application.Features.Auth.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("First name cannot be empty");
            RuleFor(x => x.LastName)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Last name cannot be empty");
            RuleFor(x => x.Email)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");
            RuleFor(x => x.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Password cannot be empty")
                .MinimumLength(4)
                .WithMessage("Password must be at least 4 characters long");
        }
    }
}
