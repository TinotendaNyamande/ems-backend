using EMS.Domain.Enums;
using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Commands.CreateEmailAccount
{
    public class CreateEmailAccountValidator:AbstractValidator<CreateEmailAccountCommand>
    {
        public CreateEmailAccountValidator()
        {
            RuleFor(x => x.EmailType)
                .IsInEnum()
                .WithMessage("Email type is not supported");
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");
            RuleFor(x => x.Password)
                .Must(x=>!string.IsNullOrWhiteSpace(x))
                .WithMessage("Password cannot be empty for this email type")
                .When(x => x.EmailType == EmailType.Gmail || x.EmailType == EmailType.Outlook || x.EmailType == EmailType.Custom);
            RuleFor(x => x.ClientId)
                .Must(x=> !string.IsNullOrWhiteSpace(x))
                .WithMessage("Client id cannot be empty for office365 or outlook accounts")
                .When(x=>x.EmailType==EmailType.Office365);
            RuleFor(x => x.ClientSecret)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Client secret cannot be empty for office365 or outlook accounts")
                .When(x => x.EmailType == EmailType.Office365 );
            RuleFor(x => x.TenantId)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Tenant Id cannot be empty for office365 or outlook accounts")
                .When(x => x.EmailType == EmailType.Office365 );

        }
    }
}
