using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataBaseContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AplikacijaDbContext>
    {
        public AplikacijaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AplikacijaDbContext>();
            optionsBuilder.UseSqlite("Data Source=ProjectManagement.db");
            return new AplikacijaDbContext(optionsBuilder.Options);
        }
    }
}
