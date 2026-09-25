using FluentValidation;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.DeleteSLAEntry
{
    public class DeleteEntryValidation : AbstractValidator<DeleteSLAEntryCommand>
    {
        public DeleteEntryValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}