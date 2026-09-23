using FluentValidation;

namespace EMS.Application.Features.Emails.Queries.GetEmailByMessageId
{
    public class GetEmailByMessageIdValidation : AbstractValidator<GetEmailByMessageIdQuery>
    {
        public GetEmailByMessageIdValidation()
        {
            RuleFor(x=>x.MessageId).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Message Id cannot be empty");
        }
    }
}