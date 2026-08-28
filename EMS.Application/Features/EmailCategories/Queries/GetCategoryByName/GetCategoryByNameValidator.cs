using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Queries.GetCategoryByName
{
    public class GetCategoryByNameValidator : AbstractValidator<GetCategoryByNameQuery>
    {
        public GetCategoryByNameValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category name is required.");        }
    }
}
