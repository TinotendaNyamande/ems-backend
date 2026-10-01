using FluentValidation;

namespace EMS.Application.Features.EmailAttachments.Commands.CreateEmailAttachment
{
    internal class CreateEmailAttachmentValidation : AbstractValidator<CreateEmailAttachmentCommand>
    {
        public CreateEmailAttachmentValidation()
        {
            RuleFor(x => x.EmailId).NotEmpty().WithMessage("EmailId is required.");
            RuleFor(x => x.FileName).NotEmpty().WithMessage("FileName is required.");
            RuleFor(x => x.FilePath).NotEmpty().WithMessage("FilePath is required.");
            RuleFor(x => x.FileType).NotEmpty().WithMessage("FileType is required.");
            RuleFor(x => x.FileSize).GreaterThan(0).WithMessage("FileSize must be greater than 0.");
        }
    }
}