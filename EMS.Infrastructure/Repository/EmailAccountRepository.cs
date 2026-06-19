using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;
namespace EMS.Infrastructure.Repository
{
    internal class EmailAccountRepository(ApplicationDbContext context) : IEmailAccountRepository
    {
        public async Task ChangeApplicationSecretAsync(Guid id, ChangeClientSecretDto changeClientSecretDto)
        {
            var email = await context.EmailAccounts.FindAsync(id) ?? throw new ResourceNotFoundException("Email", id);
            if (email.ClientSecret != changeClientSecretDto.OldSecret)
            {
                throw new InvalidOperationException("Client secret provided not matching");
            }
            email.ChangeClientSecret(changeClientSecretDto.NewSecret);
            await context.SaveChangesAsync();

        }

        public async Task ChangePasswordAsync(Guid id, ChangeEmailPasswordDto changePasswordDto)
        {
            var email = await context.EmailAccounts.FindAsync(id) ?? throw new ResourceNotFoundException("Email", id);
            if (email.Password != changePasswordDto.OldPassword)
            {
                throw new InvalidOperationException("Password provided not matching");
            }
            email.ChangePassword(changePasswordDto.NewPassword);
            await context.SaveChangesAsync();
        }

        public async Task CreateEmailAccountAsync(EmailAccount EmailAccount)
        {

            context.Add(EmailAccount);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var affectedRows = await context.EmailAccounts.Where(m => m.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                throw new ResourceNotFoundException("Email", id);
            }
        }

        public async Task<IEnumerable<EmailAccount>> GetAllValidatedAsync()
        {
            return await context.EmailAccounts.Where(m=>m.IsValidated).ToListAsync();
        }

        public async Task<EmailAccount> GetEmailAccountAsync(Guid id)
        {
            return await context.EmailAccounts
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Include(e => e.Organisation)
                .FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException("Email configuration", id);
        }

        public async Task<IEnumerable<EmailAccount>> GetEmailAccountsForOrganisationAsync(Guid organisationId)
        {
            return await context.EmailAccounts
                .AsNoTracking()
                .Where(m => m.OrganisationId == organisationId)
                .Include(m => m.Organisation)
                .ToListAsync();
        }

        public async Task MarkAsInvalidated(Guid id)
        {
            var mailConfig = context.EmailAccounts.Find(id) ?? throw new ResourceNotFoundException("Email configuration", id);
            mailConfig.Invalidate();
            await context.SaveChangesAsync();
        }

        public async Task MarkAsValidated(Guid id)
        {
            var mailConfig = context.EmailAccounts.Find(id) ?? throw new ResourceNotFoundException("Email configuration", id);
            mailConfig.Validate();
            await context.SaveChangesAsync();
        }
    }
}
