using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Infrastructure.persistence
{
    internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        internal DbSet<EmailAttachment> EmailAttachments { get; set; }
        internal DbSet<EmailCategory> EmailCategories { get; set; }
        internal DbSet<Email> Emails { get; set; }
        internal DbSet<EmailAccount> EmailAccounts { get; set; }
        internal DbSet<RefreshToken> RefreshTokens { get; set; }
        internal DbSet<EmailTask> EmailTasks { get; set; }
        internal DbSet<EmailCategoriesUserMatrix> EmailCategoriesUserMatrices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<EmailAccount>()
                .HasMany(e => e.Emails)
                .WithOne(m => m.EmailAccount)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EmailAccount>()
                .Property(e => e.EmailType)
                .HasConversion<string>();

            builder.Entity<Email>()
                .HasOne(e => e.EmailCategory)
                .WithMany(e => e.Emails)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Email>()
                .HasMany(e => e.EmailAttachments)
                .WithOne(e => e.Email)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<EmailTask>()
                .Property(e => e.Status)
                .HasConversion<string>();


        }
    }
}
