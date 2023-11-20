using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Intelogix.TimeTracker.Data.Migrations
{
    internal class TimeTrackerDbContextMigration : IDesignTimeDbContextFactory<TimeTrackerDbContext>
    {
        public TimeTrackerDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TimeTrackerDbContext> optionsBuilder = new DbContextOptionsBuilder<TimeTrackerDbContext>()
          .UseSqlServer("Server=RC-17;Database=TimeTrackerDb;Trusted_Connection=True;TrustServerCertificate=True;");
           var settings = new Dictionary<string, string> {
           {"Security:Key", "{0FA740C5-6134-456E-BCF3-53DECCC6A4BB}"}};

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
            return new TimeTrackerDbContext(optionsBuilder.Options,configuration);
        }
    }
}
