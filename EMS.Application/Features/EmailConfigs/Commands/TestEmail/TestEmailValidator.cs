using EMS.Domain.Enums;
using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Commands.TestEmail
{
    public class TestEmailValidator:AbstractValidator<TestEmailCommand>
    {
        public TestEmailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Mail box config id cannot be empty");
            RuleFor(x => x.ToEmail)
                .NotEmpty()
                .WithMessage("Receiver Email address is required")
                .EmailAddress()
                .WithMessage("Invalid email format");

        }
    }
}
