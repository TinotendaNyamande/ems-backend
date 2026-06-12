using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory
{
    public class DeleteEmailCategoryValidation : AbstractValidator<DeleteEmailCategoryCommand>
    {
        public DeleteEmailCategoryValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");

        }
    }
}
