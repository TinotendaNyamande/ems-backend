using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Commands.ChangeClientSecret
{
    public class ChangeClientSecretValidator:AbstractValidator<ChangeClientSecretCommand>
    {
        public ChangeClientSecretValidator()
        {
            RuleFor(x => x.EmailId).NotEmpty().WithMessage("Email Id cannot be empty");
            RuleFor(x => x.NewSecret).NotEmpty().WithMessage("New secret cannot be empty");
            RuleFor(x => x.OldSecret).NotEmpty().WithMessage("Old secret cannot be empty");
        }
    }
}
