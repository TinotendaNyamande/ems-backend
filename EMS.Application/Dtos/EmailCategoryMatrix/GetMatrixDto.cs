namespace EMS.Application.Dtos.EmailCategoryMatrix
{
    public class GetMatrixDto
    {
        public string CategoryName {get;set;}
        public Guid CategoryId {get;set;}
        public string UserFirstName {get;set;}
         public string UserLastName {get;set;}
        public string Userid {get;set;}
        public Guid Id {get;set;}
        public bool IsAvailable {get;set;}
        public DateTime? LastAssignedDate {get;set;}
    }
}