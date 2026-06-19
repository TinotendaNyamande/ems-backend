using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountsForOrganisation
{
    public class GetEmailAccountsForOrganisationValidator:AbstractValidator<GetEmailAccountsForOrganisationQuery>
    {
        public GetEmailAccountsForOrganisationValidator()
        {
            RuleFor(x=>x.OrganisationId).NotEmpty().WithMessage("Organisation id cannot be empty");
        }
    }
}
