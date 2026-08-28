using FluentValidation;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForAccount
{
    public class GetMatrixForAccountValidation : AbstractValidator<GetMatrixForAccountQuery>
    {
        public GetMatrixForAccountValidation()
        {
            RuleFor(x=>x.EmailAccountId).NotEmpty().WithMessage("Email account id cannot be empty");
        }
    }
}