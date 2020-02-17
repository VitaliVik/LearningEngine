using LearningEngine.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using LearningEngine.Persistence.Models;

namespace LearningEngine.Api
{
    public class DependencyResolver
    {
        public IServiceProvider ServiceProvider { get; }
        public string CurrentDirectory { get; set; }

        public DependencyResolver()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureSevices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureSevices(IServiceCollection services)
        {
            services.AddTransient<IEnviromentService, EnviromentService>();
            services.AddTransient<IConfigurationService, ConfigurationService>(provider =>
            new ConfigurationService(provider.GetService<IEnviromentService>())
            {
                CurrentDirectory = CurrentDirectory
            });

            services.AddTransient(provider =>
            {
                var configureService = provider.GetService<IConfigurationService>();
                var connectionString = configureService.GetConfiguration().GetConnectionString(nameof(LearnEngineContext));
                var optionsBuilder = new DbContextOptionsBuilder<LearnEngineContext>();
                optionsBuilder.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("LearningEngine.Persistence"));
                return new LearnEngineContext(optionsBuilder.Options);
            });

        }
    }
}
