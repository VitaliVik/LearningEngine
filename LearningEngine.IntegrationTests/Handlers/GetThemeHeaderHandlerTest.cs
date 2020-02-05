using LearningEngine.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningEngine.Domain.Query

namespace LearningEngine.IntegrationTests.Handlers
{
    public class GetThemeHeaderHandlerTest
    {
        private readonly LearnEngineContext _context;
        public GetThemeHeaderHandlerTest(LearnEngineContext context)
        {
            _context = context;
        }
        public async Task GetThemeHeaderTest()
        {
            var theme = new Theme
            {
                Name = "test_GetHeaderTest",
                Description = "test test hanlder"
            };
            _context.Themes.Add(theme);
            var query = new GetThemeHeaderQuery();
        }

    }
}
