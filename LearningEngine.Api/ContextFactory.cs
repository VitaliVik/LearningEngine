using LearningEngine.Api.Services;
using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace LearningEngine.Api
{
    public class ContextFactory : IDesignTimeDbContextFactory<LearnEngineContext>
    {
        public LearnEngineContext CreateDbContext(string[] args)
        {
            var resolver = new DependencyResolver
            {
                CurrentDirectory = Path.Combine(Directory.GetCurrentDirectory())
            };
            return resolver.ServiceProvider.GetService(typeof(LearnEngineContext)) as LearnEngineContext;
        }
    }
}
