using EMS.Domain.Enums;
using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Commands.ValidateEmailAccount
{
    public class ValidateConfigValidator:AbstractValidator<ValidateAccountCommand>
    {
        public ValidateConfigValidator()
        {
            RuleFor(x => x.EmailAccountId)
                .NotEmpty()
                .WithMessage("Email Account id cannot be empty");

        }
    }
}
