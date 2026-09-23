using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Projects.Domain.Exceptions;
namespace EMS.Infrastructure.Repository
{
    internal class EmailAccountRepository(ApplicationDbContext context, ILogger<EmailAccountRepository> logger,IEncryptionService encryptionService) : IEmailAccountRepository
    {
        public async Task ChangeApplicationSecretAsync(Guid id, ChangeClientSecretDto changeClientSecretDto)
        {
            var email = await context.EmailAccounts.FindAsync(id) ?? throw new ResourceNotFoundException("Email Account", id);
            var decryptedOldSecret = encryptionService.DecryptData(email.ClientSecret);
            if (decryptedOldSecret != changeClientSecretDto.OldSecret)
            {
                logger.LogWarning("Failed to change client secret for email {EmailId}: Old secret does not match", id);
                throw new InvalidOperationException("Incorrect old client secret provided");
            }
            email.ChangeClientSecret(changeClientSecretDto.NewSecret);
            await context.SaveChangesAsync();

        }

        public async Task ChangePasswordAsync(Guid id, ChangeEmailPasswordDto changePasswordDto)
        {
            var email = await context.EmailAccounts.FindAsync(id) ?? throw new ResourceNotFoundException("Email Account", id);
            var decryptedOldPassword = encryptionService.DecryptData(email.Password);
            if (decryptedOldPassword != changePasswordDto.OldPassword)
            {
                logger.LogWarning("Stored password {old} does not match supplied password {new}", decryptedOldPassword, changePasswordDto.OldPassword);
                throw new InvalidOperationException("Incorrect old password provided");
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
                throw new ResourceNotFoundException("Email Account", id);
            }
        }

        public async Task<IEnumerable<EmailAccount>> GetAllValidatedEmailAccountsAsync()
        {
            return await context.EmailAccounts.Where(m=>m.IsValidated).ToListAsync();
        }

        public async Task<EmailAccount> GetEmailAccountAsync(Guid id)
        {
            return await context.EmailAccounts
                .AsNoTracking()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException("Email Account", id);
        }

        public async Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync()
        {
            return await context.EmailAccounts
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task MarkAsInvalidated(Guid id)
        {
            var emailAccount = context.EmailAccounts.Find(id) ?? throw new ResourceNotFoundException("Email Account", id);
            emailAccount.Invalidate();
            await context.SaveChangesAsync();
        }

        public async Task MarkAsValidated(Guid id)
        {
            var emailAccount = context.EmailAccounts.Find(id) ?? throw new ResourceNotFoundException("Email Account", id);
            emailAccount.Validate();
            await context.SaveChangesAsync();
        }
    }
}
