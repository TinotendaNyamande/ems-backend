using EMS.Domain.Enums;
using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Commands.ValidateEmailAccount
{
    public class ValidateAccountValidator:AbstractValidator<ValidateAccountCommand>
    {
        public ValidateAccountValidator()
        {
            RuleFor(x => x.EmailAccountId)
                .NotEmpty()
                .WithMessage("Email Account id cannot be empty");

        }
    }
}
