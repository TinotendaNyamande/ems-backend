using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForAccount
{
        public class GetEmailCategoriesForOrganisationValidation:AbstractValidator<GetEmailCategoriesForAccountQuery>
    {
        public GetEmailCategoriesForOrganisationValidation()
        {
            RuleFor(x=>x.EmailAccountId).NotEmpty().WithMessage("Email account Id cannot be empty");
        }
    }
}