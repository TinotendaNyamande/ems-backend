using EMS.Domain.Enums;
using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Commands.CreateEmailConfig
{
    public class CreateEmailConfigValidator:AbstractValidator<CreateEmailConfigCommand>
    {
        public CreateEmailConfigValidator()
        {
            RuleFor(x => x.EmailType)
                .IsInEnum()
                .WithMessage("Email type is not supported");
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");
            RuleFor(x => x.OrganisationId)
                .NotEmpty()
                .WithMessage("You must create an organisation before creating email account");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty for this email type")
                .When(x => x.EmailType == EmailType.Gmail || x.EmailType == EmailType.Outlook || x.EmailType == EmailType.Custom);
            RuleFor(x => x.ClientId)
                .NotEmpty()
                .WithMessage("Client id cannot be empty for office365 or outlook accounts")
                .When(x=>x.EmailType==EmailType.Office365);
            RuleFor(x => x.ClientSecret)
                .NotEmpty()
                .WithMessage("Client secret cannot be empty for office365 or outlook accounts")
                .When(x => x.EmailType == EmailType.Office365 );
            RuleFor(x => x.TenantId)
                .NotEmpty()
                .WithMessage("Tenant Id cannot be empty for office365 or outlook accounts")
                .When(x => x.EmailType == EmailType.Office365 );

        }
    }
}
