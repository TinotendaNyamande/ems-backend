using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Commands.CloseTask
{
    public class CloseTaskValidation :AbstractValidator<CloseTaskCommand>
    {
        public CloseTaskValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x=>x.AdditionalInfo).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Additional Info cannot be empty");
        }
    }
}