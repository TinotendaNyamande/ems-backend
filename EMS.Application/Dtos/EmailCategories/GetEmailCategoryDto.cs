namespace EMS.Application.Dtos.EmailCategories
{
    public class GetEmailCategoryDto
    {
        public Guid Id { get; set; }
        public Guid EmailAccountId { get; set; }
        public string CategoryName { get; set; }
        public double SLAHours { get; set; }
    }
}