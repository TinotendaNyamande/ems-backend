using FluentValidation;

namespace EMS.Application.Features.RolesAndPermissions.Commands.EditPermission
{
    public class EditPermissionValidator:AbstractValidator<EditPermissionCommand>
    {
        public EditPermissionValidator()
        {
            RuleFor(x => x.PermissionId).NotEmpty().WithMessage("PermissionId is required.");
            RuleFor(x => x.OrganisationRoleId).NotEmpty().WithMessage("OrganisationRoleId is required.");
            RuleFor(x => x.PermissionKey).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("PermissionKey is required.");
            RuleFor(x=>x.IsAllowed).NotNull().WithMessage("IsAllowed is required.");
        }
    }
}
