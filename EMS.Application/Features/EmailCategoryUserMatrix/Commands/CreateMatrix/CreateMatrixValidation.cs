using FluentValidation;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.CreateMatrix
{
    public class CreateMatrixValidation:AbstractValidator<CreateMatrixCommand>
    {
        public CreateMatrixValidation()
        {
            RuleFor(x=>x.EmailCategoryId).NotEmpty().WithMessage("Email category cannot be empty");
            RuleFor(x=>x.UserId).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("User id cannot be empty");
        }
    }
}