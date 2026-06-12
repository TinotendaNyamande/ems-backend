using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailCategoryRepository
    {
        Task CreateEmailCategoryAsync(EmailCategory emailCategory);
        Task<IEnumerable<EmailCategory>> GetEmailCategoriesAsync(Guid organisationId);
        Task RenameEmailCategoryAsync(Guid id,string newName);
        Task DeleteEmailCategoryAsync (Guid id);
        Task<EmailCategory> GetCategoryByIdAsync (Guid id);
        
    }
}