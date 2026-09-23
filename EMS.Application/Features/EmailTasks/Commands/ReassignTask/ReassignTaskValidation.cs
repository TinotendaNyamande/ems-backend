using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Commands.ReassignTask
{
    public class ReassignTaskValidation : AbstractValidator<ReassignTaskCommand>
    {
        public ReassignTaskValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");
            RuleFor(x => x.NewUserId).NotEmpty().WithMessage("New user ID cannot be empty");
            RuleFor(x => x.UserId).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("User Id cannot be empty");

        }
    }
}