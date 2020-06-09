using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEngine.Api.Services
{
    public class EnviromentService : IEnviromentService
    {
        public EnviromentService()
        {
            EnviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT") ?? "develop";
        }

        public string EnviromentName { get; set; }
    }
}
