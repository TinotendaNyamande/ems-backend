using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Queries.GetEmail
{
    public class GetEmailValidator:AbstractValidator<GetEmailQuery>
    {
        public GetEmailValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Email account id cannot be empty");
        }
    }
}
