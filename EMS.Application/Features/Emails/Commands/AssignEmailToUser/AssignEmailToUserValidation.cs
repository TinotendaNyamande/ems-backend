using FluentValidation;

namespace EMS.Application.Features.Emails.Commands.AssignEmailToUser
{
    public class AssignEmailToUserValidation:AbstractValidator<AssignEmailToUserCommand>
    {
        public AssignEmailToUserValidation()
        {
            RuleFor(x=>x.EmailId).NotEmpty().WithMessage("Email Id caannot be empty");
            RuleFor(x=>x.EmailCategoryId).NotEmpty().WithMessage("Email category Id cannot be empty");
        }
    }
}