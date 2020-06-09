using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEngine.Api.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public IEnviromentService EnvService { get; set; }
        public string CurrentDirectory { get; set; }

        public ConfigurationService(IEnviromentService enviromentService)
        {
            EnvService = enviromentService;
        }
        public IConfiguration GetConfiguration(string directoryPath = null)
        {
            CurrentDirectory = directoryPath;
            CurrentDirectory ??= Directory.GetCurrentDirectory();
            return new ConfigurationBuilder()
                .SetBasePath(CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{EnvService.EnviromentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
