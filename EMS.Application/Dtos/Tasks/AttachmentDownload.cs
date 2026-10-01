namespace EMS.Application.Dtos.Tasks
{
    public record AttachmentDownload(
        string FilePath,
        string FileType,
        string FileName
    );
}