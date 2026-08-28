using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory
{
    public class CreateEmailCategoryValidation : AbstractValidator<CreateEmailCategoryCommand>
    {

        public CreateEmailCategoryValidation()
        {
            RuleFor(x => x.CategoryName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Category name cannot be empty");
            RuleFor(x => x.EmailAccountId).NotEmpty().WithMessage("Email Account Id cannot be empty");
        }

    }
}