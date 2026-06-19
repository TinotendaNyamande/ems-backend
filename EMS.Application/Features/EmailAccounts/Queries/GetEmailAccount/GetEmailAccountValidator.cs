using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccount
{
    public class GetEmailAccountValidator:AbstractValidator<GetEmailAccountQuery>
    {
        public GetEmailAccountValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Email account id cannot be empty");
        }
    }
}
