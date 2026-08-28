using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccounts
{
    public class GetEmailAccountsValidator:AbstractValidator<GetEmailAccountsQuery>
    {
        public GetEmailAccountsValidator()
        {
            // RuleFor(x=>x.EmailAccountId).NotEmpty().WithMessage("Email account id cannot be empty");
        }
    }
}
