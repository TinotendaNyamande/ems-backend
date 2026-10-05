using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EMS.Domain.Exceptions;
namespace EMS.Infrastructure.Repository
{
    internal class EmailAccountRepository(ApplicationDbContext context, ILogger<EmailAccountRepository> logger,IEncryptionService encryptionService) : IEmailAccountRepository
    {
        public async Task ChangeApplicationSecretAsync(Guid id, string oldSecret, string newSecret)
        {
            var email = await context.EmailAccounts.FindAsync(id) ?? throw new ResourceNotFoundException("Email Account", id);
            if (string.IsNullOrEmpty(email.ClientSecret))
            {
                logger.LogWarning("Failed to change client secret for email {EmailId}: No existing client secret found", id);
                throw new InvalidOperationException("No existing client secret found for this email account");
            }
            var decryptedOldSecret = encryptionService.DecryptData(email.ClientSecret);
            if (decryptedOldSecret != oldSecret)
            {
                logger.LogWarning("Failed to change client secret for email {EmailId}: Old secret does not match", id);
                throw new InvalidOperationException("Incorrect old client secret provided");
            }
            email.ChangeClientSecret(newSecret);

        }

        public async Task ChangePasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            var email = await context.EmailAccounts.FindAsync(id) ?? throw new ResourceNotFoundException("Email Account", id);
            if (string.IsNullOrEmpty(email.Password))
            {
                logger.LogWarning("Failed to change password for email {EmailId}: No existing password found", id);
                throw new InvalidOperationException("No existing password found for this email account");
            }
            var decryptedOldPassword = encryptionService.DecryptData(email.Password);
            if (decryptedOldPassword != oldPassword)
            {
                logger.LogWarning("Stored password {old} does not match supplied password {new}", decryptedOldPassword, oldPassword);
                throw new InvalidOperationException("Incorrect old password provided");
            }
            email.ChangePassword(newPassword);
        }

        public async Task CreateEmailAccountAsync(EmailAccount EmailAccount)
        {

            context.Add(EmailAccount);
        }

        public async Task DeleteAsync(Guid id)
        {
            var emailAccount = await context.EmailAccounts.FindAsync(id) ?? throw new ResourceNotFoundException("Email Account", id);
            context.Remove(emailAccount);
        }

        public async Task<IEnumerable<EmailAccount>> GetAllValidatedEmailAccountsAsync()
        {
            return await context.EmailAccounts.Where(m=>m.IsValidated).ToListAsync();
        }

        public async Task<EmailAccount?> GetEmailAccountByIdAsync(Guid id)
        {
            return await context.EmailAccounts
                .AsNoTracking()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
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
        }

        public async Task MarkAsValidated(Guid id)
        {
            var emailAccount = context.EmailAccounts.Find(id) ?? throw new ResourceNotFoundException("Email Account", id);
            emailAccount.Validate();
        }
    }
}
