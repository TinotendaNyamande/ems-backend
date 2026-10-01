namespace EMS.Application.Interfaces
{
    using EMS.Domain.Models;

    public interface IEmailAttachmentRepository
    {
 
        Task<EmailAttachment> CreateEmailAttachmentAsync(EmailAttachment attachment,CancellationToken cancellationToken = default);
        Task SaveRangeAsync(IEnumerable<EmailAttachment> attachments,CancellationToken cancellationToken = default);
        Task<IReadOnlyList<EmailAttachment>> GetByEmailIdAsync(Guid emailId,CancellationToken cancellationToken = default);
        Task<EmailAttachment?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid attachmentId,CancellationToken cancellationToken = default);
        Task<int> DeleteByEmailIdAsync(Guid emailId,CancellationToken cancellationToken = default);
    }
}