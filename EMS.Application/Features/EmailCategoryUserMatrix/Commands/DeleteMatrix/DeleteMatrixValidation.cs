using FluentValidation;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.DeleteMatrix
{
    public class DeleteMatrixValidation:AbstractValidator<DeleteMatrixCommand>
    {
        public DeleteMatrixValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Id cannot be empty");
        }
    }
}