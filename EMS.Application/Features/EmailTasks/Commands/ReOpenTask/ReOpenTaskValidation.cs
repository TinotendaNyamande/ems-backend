
using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Commands.ReOpenTask
{
    public class ReOpenTaskValidation :AbstractValidator<ReOpenTaskCommand>
    {
        public ReOpenTaskValidation()
        {
            RuleFor(x=>x.TaskId).NotEmpty().WithMessage("TaskId cannot be empty");
        }

    }
}