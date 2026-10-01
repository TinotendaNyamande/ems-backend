using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using Projects.Domain.Exceptions;

namespace EMS.Application.Services
{
    internal sealed class EmailAttachmentDownloadService(IEmailAttachmentRepository attachments) : IEmailAttachmentDownloadService
    {

        public async Task<AttachmentDownload> GetDownloadAsync(Guid attachmentId, CancellationToken ct = default)
        {
            var attachment = await attachments.GetByIdAsync(attachmentId, ct)
                ?? throw new ResourceNotFoundException("Attachment", attachmentId);

            return new AttachmentDownload(
                attachment.FilePath,
                attachment.FileType,
                attachment.FileName);
        }
    }
}