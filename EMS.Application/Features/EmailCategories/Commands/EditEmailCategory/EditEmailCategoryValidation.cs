using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Commands.EditEmailCategory
{
    public class EditEmailCategoryValidation : AbstractValidator<EditEmailCategoryCommand>
    {

        public EditEmailCategoryValidation()
        {
            RuleFor(x => x.NewName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Category name cannot be empty");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.SlaHours).InclusiveBetween(1, 30).WithMessage("SLA hours must be between 1 and 30");
        }

    }
}