using FluentValidation;

namespace EMS.Application.Features.UsersManagement.Commands.AssignUserRole
{
    public class AssignUserRoleValidator:AbstractValidator<AssignUserRoleCommand>
    {
        public AssignUserRoleValidator()
        {
            RuleFor(x => x.UserId)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("User ID cannot be empty");
            RuleFor(x => x.RoleId)
                .Must(x => x != Guid.Empty)
                .WithMessage("Role ID cannot be empty");
        }
    }
}
