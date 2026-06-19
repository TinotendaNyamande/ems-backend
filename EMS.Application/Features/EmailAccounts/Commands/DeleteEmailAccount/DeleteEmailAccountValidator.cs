using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Commands.DeleteEmailAccount
{
    public class DeleteEmailValidator:AbstractValidator<DeleteEmailAccountCommand>
    {
        public DeleteEmailValidator()
        {
            RuleFor(x=>x.EmailId).NotEmpty().WithMessage("Email id cannot be empty");
        }
    }
}
