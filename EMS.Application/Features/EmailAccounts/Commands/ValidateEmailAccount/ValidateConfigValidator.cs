using EMS.Domain.Enums;
using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Commands.ValidateEmailAccount
{
    public class ValidateConfigValidator:AbstractValidator<ValidateAccountCommand>
    {
        public ValidateConfigValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Mail box config id cannot be empty");

        }
    }
}
