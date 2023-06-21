using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LicenseManager.EntityFramework
{
    public class LicenseManagerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<LicenseManagerDbContext>
    {
        public LicenseManagerDbContext CreateDbContext(string[] args)
        {
            var connectionString = @$"Data Source=.\SQLEXPRESS; Initial Catalog=LicenseManager; User ID=sa; Password=Marangoni1; TrustServerCertificate=True;";
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;

            return new LicenseManagerDbContext(options);
        }
    }
}
