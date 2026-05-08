using EMS.Domain.Enums;
using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Commands.ValidateConfig
{
    public class ValidateConfigValidator:AbstractValidator<ValidateConfigCommand>
    {
        public ValidateConfigValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Mail box config id cannot be empty");

        }
    }
}
