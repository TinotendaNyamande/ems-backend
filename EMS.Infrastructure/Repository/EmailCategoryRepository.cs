using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

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
            var deletedRows = await context.EmailCategories.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (deletedRows == 0)
            {
                throw new ResourceNotFoundException("email category", id);
            }
        }

        public Task<bool> EmailCategoryExistsInEmailAccountAsync(Guid EmailAccountId, string categoryName)
        {
            var exists = context.EmailCategories.AsNoTracking().AnyAsync(e => e.EmailAccountId == EmailAccountId && e.CategoryName == categoryName);
            return exists;
        }

        public async Task<EmailCategory> GetCategoryByIdAsync(Guid id)
        {
            return await context.EmailCategories.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("email category", id);
        }

        public async Task<EmailCategory> GetCategoryByNameAsync(Guid emailAccountId,string categoryName)
        {
            return await context.EmailCategories.AsNoTracking().Where(e => e.CategoryName == categoryName && e.EmailAccountId==emailAccountId).FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("email category", categoryName);
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