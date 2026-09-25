using FluentValidation;

namespace EMS.Application.Features.Emails.Commands.CategorizeEmails
{
    public class CategorizeEmailsValidation:AbstractValidator<CategorizeEmailsCommand>
    {
        public CategorizeEmailsValidation()
        {
            RuleFor(x=>x.EmailId).NotEmpty().WithMessage("Email Id cannot be empty");
            RuleFor(x=>x.EmailAccountId).NotEmpty().WithMessage("Email account Id cannot be empty");
        }
    }
}