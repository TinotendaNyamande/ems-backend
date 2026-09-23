using FluentValidation;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.RecordUserAssignedTaskAction
{
    public class RecordUserAssignedTaskActionValidation : AbstractValidator<RecordUserAssignedTaskActionCommand>
    {
        public RecordUserAssignedTaskActionValidation()
        {
            RuleFor(x=>x.MatrixId).NotEmpty().WithMessage("Matrix Id cannot be empty");
        }
    }
}