namespace EMS.Application.Dtos.EmailCategories
{
    public class GetEmailCategoriesDto
    {
        public Guid Id { get; set; }
        public Guid OrganisationId { get; set; }
        public string CategoryName { get; set; }
    }
}