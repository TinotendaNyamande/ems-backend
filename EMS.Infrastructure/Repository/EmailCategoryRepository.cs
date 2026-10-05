using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using EMS.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailCategoryRepository(ApplicationDbContext context) : IEmailCategoryRepository
    {
        public async Task<EmailCategory> CreateEmailCategoryAsync(EmailCategory emailCategory)
        {
            context.Add(emailCategory);
            return emailCategory;
        }

        public async Task DeleteEmailCategoryAsync(Guid id)
        {
            var emailCategory = await context.EmailCategories.FindAsync(id) ?? throw new ResourceNotFoundException("email category", id);
            context.Remove(emailCategory);
        }

        public Task<bool> EmailCategoryExistsInEmailAccountAsync(Guid EmailAccountId, string categoryName)
        {
            var exists = context.EmailCategories.AsNoTracking().AnyAsync(e => e.EmailAccountId == EmailAccountId && e.CategoryName == categoryName);
            return exists;
        }

        public async Task<EmailCategory?> GetCategoryByIdAsync(Guid id)
        {
            return await context.EmailCategories.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync() ;
            
        }

        public async Task<EmailCategory?> GetCategoryByNameAsync(Guid emailAccountId,string categoryName)
        {
            return await context.EmailCategories.AsNoTracking().Where(e => e.CategoryName == categoryName && e.EmailAccountId==emailAccountId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmailCategory>> GetEmailCategoriesByEmailAccountAsync(Guid emailAccountId)
        {
            return await context.EmailCategories.AsNoTracking().Where(c => c.EmailAccountId == emailAccountId).ToListAsync();
        }

        public async Task EditEmailCategoryAsync(Guid id, string newName, int slaHours)
        {
            var emailCategory = await context.EmailCategories.Where(e => e.Id == id).FirstOrDefaultAsync() ??
                throw new ResourceNotFoundException("email category", id);
            emailCategory.ChangeCategoryName(newName);
            emailCategory.ChangeSLAHours(slaHours);
        }
    }
}