using FluentValidation;

namespace EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry
{
    public class CreateAuditTrailEntryValidation:AbstractValidator<CreateAuditTrailEntryCommand>
    {
        public CreateAuditTrailEntryValidation()
        {
            RuleFor(x=>x.TaskId).NotEmpty().WithMessage("Task id cannot be empty");
            RuleFor(x=>x.UserId).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("User Id cannot be empty");
            RuleFor(x=>x.Comments).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Autit comment cannot be empty");

        }
    }
}