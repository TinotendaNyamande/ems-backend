using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory
{
    public class DeleteEmailCategoryValidation : AbstractValidator<DeleteEmailCategoryCommand>
    {
        public DeleteEmailCategoryValidation()
        {
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.NewCategoryId).NotEmpty().WithMessage("New category Id cannot be empty");

        }
    }
}
