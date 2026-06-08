using FluentValidation;

namespace EMS.Application.Features.RolesAndPermissions.Queries.GetRolesForOrganisation
{
    public class GetRolesForOrganisationValidator:AbstractValidator<GetRolesForOrganisationQuery>
    {
        public GetRolesForOrganisationValidator()
        {
            RuleFor(x => x.OrganisationId).NotEmpty().WithMessage("OrganisationId is required.");
        }
    }
}
