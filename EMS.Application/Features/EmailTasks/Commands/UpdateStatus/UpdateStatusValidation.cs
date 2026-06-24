using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    public class UpdateStatusValidation:AbstractValidator<UpdateStatusCommand>
    {
        public UpdateStatusValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x=>x.NewStatus).Must(x=>!string.IsNullOrWhiteSpace(x.ToString())).WithMessage("New status cannot be empty");
        }
    }
}