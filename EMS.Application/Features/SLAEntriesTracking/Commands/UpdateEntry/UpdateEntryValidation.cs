using FluentValidation;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.UpdateEntry
{
    public class UpdateEntryValidation : AbstractValidator<UpdateEntryCommand>
    {
        public UpdateEntryValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
            RuleFor(x => x.Comments).NotEmpty().WithMessage("Comments is required.");
        }
    }
}