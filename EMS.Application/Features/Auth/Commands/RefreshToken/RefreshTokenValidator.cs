using FluentValidation;

namespace EMS.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.RefreshToken)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Refresh token is missing");
        }
    }
}
