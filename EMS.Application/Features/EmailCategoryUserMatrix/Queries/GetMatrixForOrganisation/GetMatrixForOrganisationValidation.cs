using FluentValidation;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForOrganisation
{
    public class GetMatrixForOrganisationValidation : AbstractValidator<GetMatrixForOrganisationQuery>
    {
        public GetMatrixForOrganisationValidation()
        {
            RuleFor(x=>x.OrganisationId).NotEmpty().WithMessage("Organisation id cannot be empty");
        }
    }
}