using FluentValidation;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountById
{
    public class GetEmailAccountByIdValidator:AbstractValidator<GetEmailAccountByIdQuery>
    {
        public GetEmailAccountByIdValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Email account id cannot be empty");
        }
    }
}
