using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    public class DatabaseFixture
    {
        private DbContextOptionsBuilder<LearnEngineContext> _optionsBuilder;
        public DatabaseFixture()
        {
            _optionsBuilder = new DbContextOptionsBuilder<LearnEngineContext>();
            _optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LearningEngineTestDb;Trusted_Connection=true");
            _context = new LearnEngineContext(_optionsBuilder.Options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }


        private LearnEngineContext _context;
        public LearnEngineContext Context 
        { 
            get 
            { 
                _context = new LearnEngineContext(_optionsBuilder.Options);
                return _context;
            } 
        }

    }

}
