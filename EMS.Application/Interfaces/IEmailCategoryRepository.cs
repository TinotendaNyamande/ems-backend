using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailCategoryRepository
    {
        Task<EmailCategory> CreateEmailCategoryAsync(EmailCategory emailCategory);
        Task<IEnumerable<EmailCategory>> GetEmailCategoriesAsync(Guid organisationId);
        Task EditEmailCategoryAsync(Guid id,string newName, int slaHours);
        Task DeleteEmailCategoryAsync (Guid id);
        Task<EmailCategory> GetCategoryByIdAsync (Guid id);
        Task<EmailCategory> GetCategoryByNameAsync (string categoryName);
        Task<bool> EmailCategoryExistsInEmailAccountAsync(Guid EmailAccountId, string categoryName);
        
    }
}