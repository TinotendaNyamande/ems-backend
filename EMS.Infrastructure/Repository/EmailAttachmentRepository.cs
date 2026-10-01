namespace EMS.Infrastructure.Repository
{
    using EMS.Application.Interfaces;
    using EMS.Domain.Models;
    using EMS.Infrastructure.persistence;
    using Microsoft.EntityFrameworkCore;

    internal sealed class EmailAttachmentRepository(ApplicationDbContext context) : IEmailAttachmentRepository
    {


        public async Task<EmailAttachment> CreateEmailAttachmentAsync(
            EmailAttachment attachment,
            CancellationToken cancellationToken = default)
        {
            context.EmailAttachments.Add(attachment);
            return attachment;
        }

        public async Task SaveRangeAsync(
            IEnumerable<EmailAttachment> attachments,
            CancellationToken cancellationToken = default)
        {
            await context.EmailAttachments.AddRangeAsync(attachments, cancellationToken);
        }

        public async Task<IReadOnlyList<EmailAttachment>> GetByEmailIdAsync(
            Guid emailId,
            CancellationToken cancellationToken = default)
        {
            return await context.EmailAttachments
                .AsNoTracking()
                .Where(a => a.EmailId == emailId)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(
            Guid attachmentId,
            CancellationToken cancellationToken = default)
        {
            var row = await context.EmailAttachments
                .FirstOrDefaultAsync(a => a.Id == attachmentId, cancellationToken);

            if (row is null) return false;

            context.EmailAttachments.Remove(row);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<int> DeleteByEmailIdAsync(
            Guid emailId,
            CancellationToken cancellationToken = default)
        {
            return await context.EmailAttachments
                .Where(a => a.EmailId == emailId)
                .ExecuteDeleteAsync(cancellationToken);
        }
        public async Task<EmailAttachment?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await context.EmailAttachments
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}