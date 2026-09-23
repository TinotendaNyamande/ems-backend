using FluentValidation;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.AssignEmailCategory
{
    public class AssignEmailCategoryValidation:AbstractValidator<AssignEmailCategoryCommand>
    {
        public AssignEmailCategoryValidation()
        {
            RuleFor(x=>x.EmailId).NotEmpty().WithMessage("Email Id cannot be empty");
            RuleFor(x=>x.CategoryId).NotEmpty().WithMessage("Category Id cannot be empty");
        }
    }
}