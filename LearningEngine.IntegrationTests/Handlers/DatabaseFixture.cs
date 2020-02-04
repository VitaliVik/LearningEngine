using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    public class DatabaseFixture
    {
        private DbContextOptionsBuilder<LearnEngineContext> _optionsBuilder;
        public DatabaseFixture()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json")
                .Build();
            _optionsBuilder = new DbContextOptionsBuilder<LearnEngineContext>();
            _optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }


        public LearnEngineContext Context 
        { 
            get => new LearnEngineContext(_optionsBuilder.Options);
        }

    }

}
