using FluentValidation;

namespace EMS.Application.Features.RolesAndPermissions.Queries.GetPermissionsForRole
{
    public class GetPermissionsForRoleValidator : AbstractValidator<GetPermissionsForRoleQuery>
    {
        public GetPermissionsForRoleValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role ID is required.");
        }
    }
}
