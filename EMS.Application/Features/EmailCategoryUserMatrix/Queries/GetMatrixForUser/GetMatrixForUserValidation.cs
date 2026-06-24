using FluentValidation;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForUser
{
    public class GetMatrixForUserValidation : AbstractValidator<GetMatrixForUserQuery>
    {
        public GetMatrixForUserValidation()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId cannot be empty");
        }
    }
}