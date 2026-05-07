using FluentValidation;

namespace EMS.Application.Features.Auth.Commands.Logout
{
    public class LogoutValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutValidator()
        {
            RuleFor(x => x.RefreshToken)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Refresh token is missing");
        }
    }
}
