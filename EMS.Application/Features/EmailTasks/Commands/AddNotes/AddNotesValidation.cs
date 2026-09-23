using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Commands.AddNotes
{
    public class AddNotesValidation:AbstractValidator<AddNotesCommand>
    {
        public AddNotesValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("ID cannot be empty");
            RuleFor(x=>x.AdditionalInfo).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("Notes field cannot be empty");
            RuleFor(x=>x.UserId).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("User Id cannot be empty");
        }
    }
}