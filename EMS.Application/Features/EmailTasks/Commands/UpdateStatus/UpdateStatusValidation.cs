using EMS.Domain.Enums;
using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    public class UpdateStatusValidation : AbstractValidator<UpdateStatusCommand>
    {
        public UpdateStatusValidation()
        {
            RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty");
            RuleFor(x => x.NewStatus)
            .Must(x => !string.IsNullOrWhiteSpace(x.ToString()))
            .WithMessage("New status cannot be empty");
            RuleFor(x => x.AdditionalInformation)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Additional information cannot be empty")
            .When(x => x.NewStatus == TaskStatusList.Closed);
            RuleFor(x => x.UserId).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("User Id cannot be empty");

        }
    }
}