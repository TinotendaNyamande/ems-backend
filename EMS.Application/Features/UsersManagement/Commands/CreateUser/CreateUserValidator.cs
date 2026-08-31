using FluentValidation;

namespace EMS.Application.Features.UsersManagement.Commands.CreateUser
{
    public class CreateUserValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("First name cannot be empty");
            RuleFor(x => x.LastName)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Last name cannot be empty");
            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("Role is required.")
                .Must(role => role == "Admin" || role == "Supervisor" || role == "Member")
                .WithMessage("Invalid role specified. Role must be either 'Admin', 'Supervisor', or 'Member'.");
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
