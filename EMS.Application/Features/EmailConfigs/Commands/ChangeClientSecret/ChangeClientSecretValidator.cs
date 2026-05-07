using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangeClientSecret
{
    public class ChangeClientSecretValidator : AbstractValidator<ChangeClientSecretCommand>
    {
        public ChangeClientSecretValidator()
        {
            RuleFor(x => x.EmailId)
                .NotEmpty()
                .WithMessage("Email Id cannot be empty");
            RuleFor(x => x.NewSecret)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("New secret cannot be empty");
            RuleFor(x => x.OldSecret)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Old secret cannot be empty");
        }
    }
}
