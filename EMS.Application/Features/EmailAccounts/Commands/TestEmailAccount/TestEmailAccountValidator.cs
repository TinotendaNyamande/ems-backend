using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Commands.TestEmailAccount
{
    public class TestEmailValidator:AbstractValidator<TestEmailAccountCommand>
    {
        public TestEmailValidator()
        {
            RuleFor(x => x.EmailAccountId)
                .NotEmpty()
                .WithMessage("Email account id cannot be empty");
            RuleFor(x => x.ToEmail)
                .NotEmpty()
                .WithMessage("Receiver Email address is required")
                .EmailAddress()
                .WithMessage("Invalid email format");

        }
    }
}
