using FluentValidation;

namespace EMS.Application.Features.UsersManagement.Commands.ChangeRole
{
    public class ChangeRoleValidator : AbstractValidator<ChangeRoleCommand>
    {
        public ChangeRoleValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required.");

            RuleFor(x => x.NewRole)
                .NotEmpty()
                .WithMessage("New role is required.")
                .Must(role => role == "Admin" || role == "Supervisor" || role == "Member")
                .WithMessage("Invalid role specified. Role must be either 'Admin', 'Supervisor', or 'Member'.");
        }
    }
}