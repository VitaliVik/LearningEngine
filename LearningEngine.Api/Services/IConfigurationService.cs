using Microsoft.Extensions.Configuration;

namespace LearningEngine.Api.Services
{
    public interface IConfigurationService
    {
        IConfiguration GetConfiguration(string directoryPath = null);
    }
}