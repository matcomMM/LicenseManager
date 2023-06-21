using Microsoft.EntityFrameworkCore;

namespace LicenseManager.EntityFramework
{ 
    public class LicenseManagerDbContextFactory
    {
        private readonly string _connectionString;

        public LicenseManagerDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LicenseManagerDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(_connectionString)
                                                                    .EnableSensitiveDataLogging()
                                                                    .Options;;
            return new LicenseManagerDbContext(options);
        }
    }
}
