using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailCategoryRepository(ApplicationDbContext context) : IEmailCategoryRepository
    {
        public async Task CreateEmailCategoryAsync(EmailCategory emailCategory)
        {
            context.Add(emailCategory);
            await context.SaveChangesAsync();
        }

        public async Task DeleteEmailCategoryAsync(Guid id)
        {
            var deletedRows = await context.EmailCategories.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (deletedRows == 0)
            {
                throw new ResourceNotFoundException("email category", id);
            }
        }

        public async Task<EmailCategory> GetCategoryByIdAsync(Guid id)
        {
            return await context.EmailCategories.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync() ??
            throw new ResourceNotFoundException("email category", id);
        }

        public async Task<IEnumerable<EmailCategory>> GetEmailCategoriesAsync(Guid organisationId)
        {
            return await context.EmailCategories.AsNoTracking().Where(o => o.OrganisationId == organisationId).ToListAsync();
        }

        public async Task RenameEmailCategoryAsync(Guid id, string newName)
        {
            var emailCategory = await context.EmailCategories.Where(e => e.Id == id).FirstOrDefaultAsync() ??
                throw new ResourceNotFoundException("email category", id);
            emailCategory.ChangeCategoryName(newName);
            await context.SaveChangesAsync();
        }
    }
}