using FluentValidation;

namespace EMS.Application.Features.Emails.Commands.ReadEmailsFromInbox
{
    public class ReadEmailsFromInboxValidation : AbstractValidator<ReadEmailsFromInboxCommand>
    {
        public ReadEmailsFromInboxValidation()
        {
            RuleFor(x=>x.EmailAccount.Password).NotEmpty().WithMessage("Email account password cannot be empty");
            RuleFor(x=>x.EmailAccount.Id).NotEmpty().WithMessage("Email account Id cannot be empty");
            RuleFor(x=>x.EmailAccount.EmailType).NotEmpty().WithMessage("Email account email type cannot be empty");
        }
    }
}