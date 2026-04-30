using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EMS.Infrastructure.persistence
{
    internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :IdentityDbContext<ApplicationUser>(options)
    {
        internal DbSet<Organisation> Organisations { get; set; }
        internal DbSet<EmailAttachment> EmailAttachments { get; set; }
        internal DbSet<EmailCategory> EmailCategories { get; set; }
        internal DbSet<EmailInbox> EmailInboxes { get; set; }
        internal DbSet<MailBoxConfig> MailBoxConfigs { get; set; }

       protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Organisation>()
                .HasMany(c => c.EmailCategories)
                .WithOne(o => o.Organisation)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MailBoxConfig>()
                .HasMany(e => e.EmailInboxes)
                .WithOne(m => m.MailBoxConfig)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MailBoxConfig>()
                .HasOne(o => o.Organisation)
                .WithMany(m => m.MailBoxes)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EmailInbox>()
                .HasOne(e=>e.EmailCategory)
                .WithMany(e=>e.EmailInboxes)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<EmailInbox>()
                .HasMany(e=>e.EmailAttachments)
                .WithOne(e=>e.EmailInbox)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
