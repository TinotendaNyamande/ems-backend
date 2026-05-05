using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Queries.GetEmailForOrganisation
{
    public class GetEmailForOrganisationValidator:AbstractValidator<GetEmailForOrganisationQuery>
    {
        public GetEmailForOrganisationValidator()
        {
            RuleFor(x=>x.OrganisationId).NotEmpty().WithMessage("Organisation id cannot be empty");
        }
    }
}
