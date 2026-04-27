using ems.domain.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Infrastructure.persistence
{
    internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :IdentityDbContext<ApplicationUser>
    {
        public DbSet<Organisation> Organisations { get; set; }
    }
}
