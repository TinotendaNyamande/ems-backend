using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Infrastructure.persistence
{
    internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        internal DbSet<Organisation> Organisations { get; set; }
        internal DbSet<EmailAttachment> EmailAttachments { get; set; }
        internal DbSet<EmailCategory> EmailCategories { get; set; }
        internal DbSet<Email> Emails { get; set; }
        internal DbSet<EmailAccount> EmailAccounts { get; set; }
        internal DbSet<JoinRequest> JoinRequests { get; set; }
        internal DbSet<OrganisationRole> OrganisationRoles { get; set; }
        internal DbSet<OrganisationRolePermission> OrganisationRolePermissions { get; set; }
        internal DbSet<OrganisationUserRole> OrganisationUserRoles { get; set; }
        internal DbSet<RefreshToken> RefreshTokens { get; set; }
        internal DbSet<EmailTask> EmailTasks { get; set; }
        internal DbSet<EmailCategoriesUserMatrix> EmailCategoriesUserMatrices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Organisation>()
                .HasMany(c => c.EmailCategories)
                .WithOne(o => o.Organisation)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EmailAccount>()
                .HasMany(e => e.Emails)
                .WithOne(m => m.EmailAccount)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EmailAccount>()
                .HasOne(o => o.Organisation)
                .WithMany(m => m.EmailAccounts)
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
            builder.Entity<JoinRequest>()
                .HasOne(j => j.Organisation)
                .WithMany(o => o.JoinRequests)
                .HasForeignKey(j => j.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<JoinRequest>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(j => j.RequestById)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<JoinRequest>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(j => j.ApprovedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<JoinRequest>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(j => j.RejectedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>()
                .HasOne<Organisation>()
                .WithMany()
                .HasForeignKey(o => o.OrganisationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<OrganisationUserRole>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
