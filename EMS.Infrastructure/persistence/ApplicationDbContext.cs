using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        internal DbSet<JoinRequest> JoinRequests { get; set; }
        internal DbSet<OrganisationRole> OrganisationRoles { get; set; }
        internal DbSet<OrganisationRolePermission> OrganisationRolePermissions { get; set; }
        internal DbSet<OrganisationUserRole> OrganisationUserRoles { get; set; }
        internal DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Organisation>()
                .HasMany(c => c.EmailCategories)
                .WithOne(o => o.Organisation)
                .OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<Organisation>()
            //    .HasOne<ApplicationUser>()
            //    .WithMany()
            //    .HasForeignKey(o=>o.OwnerId)
            //    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<MailBoxConfig>()
                .HasMany(e => e.EmailInboxes)
                .WithOne(m => m.MailBoxConfig)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MailBoxConfig>()
                .HasOne(o => o.Organisation)
                .WithMany(m => m.MailBoxes)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MailBoxConfig>()
                .Property(e => e.EmailType)
                .HasConversion<string>();

            builder.Entity<EmailInbox>()
                .HasOne(e=>e.EmailCategory)
                .WithMany(e=>e.EmailInboxes)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<EmailInbox>()
                .HasMany(e=>e.EmailAttachments)
                .WithOne(e=>e.EmailInbox)
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
                .HasForeignKey(o=>o.OrganisationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<OrganisationUserRole>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
