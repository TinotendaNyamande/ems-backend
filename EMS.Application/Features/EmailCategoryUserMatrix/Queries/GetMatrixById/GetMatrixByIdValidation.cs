using FluentValidation;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixById
{
    public class GetMatrixByIdValidation : AbstractValidator<GetMatrixByIdQuery>
    {
        public GetMatrixByIdValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Matrix id cannot be empty");
        }
    }
}