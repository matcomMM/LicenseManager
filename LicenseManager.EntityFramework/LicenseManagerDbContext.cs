using LicenseManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LicenseManager.EntityFramework
{
    public class LicenseManagerDbContext : DbContext
    {
        public LicenseManagerDbContext(DbContextOptions options) : base(options) { }

        public DbSet<License> Licenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<License>().OwnsOne(f => f.Feature);

            base.OnModelCreating(modelBuilder);
        }
    }

}
