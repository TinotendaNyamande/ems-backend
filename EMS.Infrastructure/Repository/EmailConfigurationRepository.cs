using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailConfigurationRepository(ApplicationDbContext context) : IEmailConfigurationRepository
    {
        public async Task ChangeApplicationSecretAsync(Guid id, ChangeClientSecretDto changeClientSecretDto)
        {
            var email = await context.MailBoxConfigs.FindAsync(id)??throw  new ResourceNotFoundException("Email",id);
            if(email.ClientSecret != changeClientSecretDto.OldSecret)
            {
                throw new InvalidOperationException("Client secret provided not matching");
            }
            email.ChangeClientSecret(changeClientSecretDto.NewSecret);
            await context.SaveChangesAsync();

        }

        public async Task ChangePasswordAsync(Guid id, ChangePasswordDto changePasswordDto)
        {
            var email = await context.MailBoxConfigs.FindAsync(id) ?? throw new ResourceNotFoundException("Email", id);
            if (email.Password != changePasswordDto.OldPassword)
            {
                throw new InvalidOperationException("Password provided not matching");
            }
            email.ChangePassword(changePasswordDto.NewPassword) ;
            await context.SaveChangesAsync();
        }

        public async Task CreateEmailAccountAsync(MailBoxConfig mailBoxConfig)
        {
            context.Add(mailBoxConfig);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var affectedRows = await context.MailBoxConfigs.Where(m=>m.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                throw new ResourceNotFoundException("Email", id);
            }
        }

        public async Task<MailBoxConfig> GetEmailAccountAsync(Guid id)
        {
            return await context.MailBoxConfigs
                .AsNoTracking()
                .Where(e=>e.Id==id)
                .Include(e=>e.Organisation)
                .FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException("Email configuration", id);
        }

        public async Task<IEnumerable<MailBoxConfig>> GetEmailAccountsForOrganisationAsync(Guid organisationId)
        {
            return await context.MailBoxConfigs
                .AsNoTracking()
                .Where(m => m.OrganisationId == organisationId)
                .Include(m => m.Organisation)
                .ToListAsync();
        }
    }
}
