namespace EMS.Application.Dtos.EmailCategoryMatrix
{
    public record GetMatrixDto(string CategoryName, Guid CategoryId, string UserFirstName, string UserLastName, string Userid, Guid Id, bool IsAvailable, DateTime? LastAssignedDate);
      
}