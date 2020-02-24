using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEngine.IntegrationTests.Fixtures
{
    public class BaseDatabaseFixture<TContext>
        where TContext : DbContext
    {
        private readonly Func<TContext> _createContext;

        public BaseDatabaseFixture(Func<DbContextOptions<TContext>, TContext> createContext)
        {
            var options = GetOptions();

            _createContext = () => createContext(options);

            var context = _createContext();

            if (!context.Database.CanConnect())
            {
                context.Database.EnsureCreated();
            }

            var pendingMigration = context.Database.GetPendingMigrations().ToList();

            if (pendingMigration.Any())
            {
                var migrator = context.Database.GetService<IMigrator>();
                pendingMigration.ForEach(migration => migrator.Migrate(migration));
            }

        }

        private DbContextOptions<TContext> GetOptions()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));

            return optionsBuilder.Options;
        }

        public async Task UseDbContext(Func<TContext, Task> useContext)
        {
            using var context = _createContext();
            using var transaction = await context.Database.BeginTransactionAsync();

            await useContext(context);

            await transaction.RollbackAsync();
        }
    }
}
