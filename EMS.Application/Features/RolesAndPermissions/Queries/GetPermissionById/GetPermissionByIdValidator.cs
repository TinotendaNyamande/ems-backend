using FluentValidation;

namespace EMS.Application.Features.RolesAndPermissions.Queries.GetPermissionById
{
    public class GetPermissionByIdValidator:AbstractValidator<GetPermissionByIdQuery>
    {
        public GetPermissionByIdValidator()
        {
            RuleFor(x => x.PermissionId).NotEmpty().WithMessage("Permission Id is required.");
        }
    }
}
