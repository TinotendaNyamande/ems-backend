using FluentValidation;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry
{
    public class CreateSLAEntryValidation:AbstractValidator<CreateSLAEntryCommand>
    {
        public CreateSLAEntryValidation()
        {
            RuleFor(x=>x.EmailTaskId).NotEmpty().WithMessage("Email Task Id cannot be empty");
            RuleFor(x=>x.Comments).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Comment cannot be empty");
        }
    }
}
