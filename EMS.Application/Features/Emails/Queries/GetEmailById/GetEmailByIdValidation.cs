using FluentValidation;

namespace EMS.Application.Features.Emails.Queries.GetEmailById
{
    public class GetEmailByIdValidation : AbstractValidator<GetEmailByIdQuery>
    {
        public GetEmailByIdValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Message Id cannot be empty");
        }
    }
}