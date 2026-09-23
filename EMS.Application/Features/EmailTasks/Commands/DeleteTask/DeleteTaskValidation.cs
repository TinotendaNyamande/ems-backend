
using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Commands.DeleteTask
{
    public class DeleteTaskValidation : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.UserId).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("User Id cannot be empty");

        }

    }
}