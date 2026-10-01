namespace EMS.Application.Dtos.Tasks 
{
    public record GetAttachmentDTO(
        Guid Id,
        string FileName,
        string FileType,
        long FileSize
    );
  
}