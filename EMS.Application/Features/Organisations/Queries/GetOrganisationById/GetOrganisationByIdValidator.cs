using FluentValidation;

namespace EMS.Application.Features.Organisations.Queries.GetOrganisationById
{
    public class GetOrganisationByIdValidator:AbstractValidator<GetOrganisationByIdQuery>
    {
        public GetOrganisationByIdValidator()
        {
            RuleFor(x => x.OrganisationId).NotEmpty().WithMessage("Organisation id cannot be empty");
        }
    }
}
