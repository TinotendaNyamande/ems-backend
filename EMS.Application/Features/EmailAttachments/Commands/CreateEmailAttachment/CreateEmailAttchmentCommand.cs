using EMS.Application.Abstractions;

namespace EMS.Application.Features.EmailAttachments.Commands.CreateEmailAttachment
{
public record CreateEmailAttachmentCommand(Guid EmailId, string FileName, string FilePath, string FileType, long FileSize) : ICommand;
}