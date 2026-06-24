using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Commands.CreateTask
{
    public class CreateTaskValidation : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidation()
        {
            RuleFor(x => x.EmailId).NotEmpty().WithMessage("Email ID cannot be empty");
            RuleFor(x => x.AssignedToUser).NotEmpty().WithMessage("Assigned user ID cannot be empty");
        }
    }
}