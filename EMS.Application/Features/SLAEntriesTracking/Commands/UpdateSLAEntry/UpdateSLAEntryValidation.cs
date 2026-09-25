using FluentValidation;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry
{
    public class UpdateSLAEntryValidation : AbstractValidator<UpdateSLAEntryCommand>
    {
        public UpdateSLAEntryValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
        }
    }
}