namespace EMS.Application.Dtos.Emails
{
    public record EmailAttachmentDto(string FileName,string FileType,long FileSize,string FilePath);

}