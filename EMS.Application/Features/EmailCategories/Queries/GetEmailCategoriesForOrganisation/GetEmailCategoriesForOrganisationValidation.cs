using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForOrganisation
{
        public class GetEmailCategoriesForOrganisationValidation:AbstractValidator<GetEmailCategoriesForOrganisationQuery>
    {
        public GetEmailCategoriesForOrganisationValidation()
        {
            RuleFor(x=>x.OrganisationId).NotEmpty().WithMessage("Organisation Id cannot be empty");
        }
    }
}