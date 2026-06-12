using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Commands.RenameEmailCategory
{
    public class RenameEmailCategoryValidation : AbstractValidator<RenameEmailCategoryCommand>
    {

        public RenameEmailCategoryValidation()
        {
            RuleFor(x => x.NewName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Category name cannot be empty");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
        }

    }
}