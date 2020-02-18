using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    public class DatabaseFixture : IDisposable
    {
        private DbContextOptionsBuilder<LearnEngineContext> _optionsBuilder;
        private LearnEngineContext _context;
        private IDbContextTransaction _currentTransaction;
        
       
        public DatabaseFixture()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json")
                .Build();
            _optionsBuilder = new DbContextOptionsBuilder<LearnEngineContext>();
            _optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
            _context = new LearnEngineContext(_optionsBuilder.Options);
            if (!_context.Database.CanConnect())
            {
                _context.Database.EnsureCreated();
            }
        }



        public LearnEngineContext Context 
        {
            get
            {
                if (_currentTransaction == null)
                {
                    _currentTransaction = _context.Database.BeginTransaction();
                    return _context;
                }
                else
                {
                    _currentTransaction.Rollback();
                    _context = new LearnEngineContext(_optionsBuilder.Options);
                    _currentTransaction = _context.Database.BeginTransaction();
                    return _context;
                }
            }
        }

        public void Dispose()
        {
            _currentTransaction.Rollback();
        }
    }

}
