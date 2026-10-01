using EMS.Application.Dtos.Tasks;

namespace EMS.Application.Interfaces
{
    public interface IEmailAttachmentDownloadService
    {
        Task<AttachmentDownload> GetDownloadAsync(Guid attachmentId, CancellationToken ct = default);

    }
}