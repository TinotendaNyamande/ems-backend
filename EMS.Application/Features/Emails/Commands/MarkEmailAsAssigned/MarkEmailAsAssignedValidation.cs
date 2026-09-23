using FluentValidation;

namespace EMS.Application.Features.Emails.Commands.MarkEmailAsAssigned
{
    public class MarkEmailAsAssignedValidation : AbstractValidator<MarkEmailAsAssignedCommand>
    {
        public MarkEmailAsAssignedValidation()
        {
            RuleFor(x=>x.EmailId).NotEmpty().WithMessage("Email ID cannot be empty");
        }
    }
}