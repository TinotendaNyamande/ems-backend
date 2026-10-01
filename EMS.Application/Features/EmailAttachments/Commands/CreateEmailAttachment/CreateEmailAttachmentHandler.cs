using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;

namespace EMS.Application.Features.EmailAttachments.Commands.CreateEmailAttachment
{
    internal class CreateEmailAttchmentCommandHandler(IEmailAttachmentRepository emailAttachmentRepository) : ICommandHandler<CreateEmailAttachmentCommand>
    {
        public async Task Handle(CreateEmailAttachmentCommand request, CancellationToken cancellationToken)
        {
            var emailAttachment = new EmailAttachment(request.EmailId, request.FileName, request.FilePath, request.FileType, request.FileSize);
            await emailAttachmentRepository.CreateEmailAttachmentAsync(emailAttachment, cancellationToken);
        }
    }
}