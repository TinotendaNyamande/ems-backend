using FluentValidation;

namespace EMS.Application.Features.UsersManagement.Queries.GetUsersForOrganisationQuery
{
    public class GetUsersForOrganisationValidator:AbstractValidator<GetUsersForOrganisationQuery>
    {
        public GetUsersForOrganisationValidator()
        {
            RuleFor(x => x.OrganisationId)
                .NotEmpty().WithMessage("OrganisationId is required.");
        }
    }
}
