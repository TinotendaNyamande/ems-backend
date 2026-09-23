

using FluentValidation;

namespace EMS.Application.Features.Emails.Commands.CreateEmail
{
    public class CreateEmailValidation:AbstractValidator<CreateEmailCommand>
    {
        public CreateEmailValidation()
        {
            RuleFor(x=>x.FromEmail).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Source email address is required");
             RuleFor(x=>x.ToEmail).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Destination email address is required");
              RuleFor(x=>x.Subject).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Email subject is required");
              RuleFor(x=>x.Body).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Email body is required");
              RuleFor(x=>x.ExternalMessageId).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Message Id is required");
              RuleFor(x=>x.EmailAccountId).NotEmpty().WithMessage("Email account ID is required");
        }
    }
}