using FluentValidation;

namespace EMS.Application.Features.Emails.Commands.ChangeEmailCategory
{
    public class ChangeEmailCategoryValidation : AbstractValidator<ChangeEmailCategoryCommand>
    {
        public ChangeEmailCategoryValidation()
        {
            RuleFor(x=>x.EmailId).NotEmpty().WithMessage("Email id cannot be empty");
            RuleFor(x=>x.NewCategoryId).NotEmpty().WithMessage("New category id cannot be empty");
        }
    }
}