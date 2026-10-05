namespace EMS.Application.Dtos.EmailCategories
{
    public record GetEmailCategoryDto(Guid Id, Guid EmailAccountId, string CategoryName, double SLAHours);
}