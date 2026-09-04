using FluentValidation;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.DeleteEntry
{
    public class DeleteEntryValidation : AbstractValidator<DeleteEntryCommand>
    {
        public DeleteEntryValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}