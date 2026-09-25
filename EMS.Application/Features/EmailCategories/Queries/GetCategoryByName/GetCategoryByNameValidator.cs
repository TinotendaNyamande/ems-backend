using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Queries.GetCategoryByName
{
    public class GetCategoryByNameValidator : AbstractValidator<GetCategoryByNameQuery>
    {
        public GetCategoryByNameValidator()
        {
            RuleFor(x => x.CategoryName).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Category name is required.");
            RuleFor(x=>x.EmailAccountId).NotEmpty().WithMessage("Email account id cannot be empty");
        }
    }
}
