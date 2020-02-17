using LearningEngine.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningEngine.Domain.Query;
using LearningEngine.Persistence.Handlers;
using System.Threading;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class GetThemeHeaderHandlerTest
    {
        private readonly LearnEngineContext _context;
        public GetThemeHeaderHandlerTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetThemeHeaderTest()
        {
            var parentTheme = new Theme
            {
                Name = "testParent",
                Description = "testParentDescription"
            };
            var theme = new Theme
            {
                Name = "test_GetHeaderTest",
                Description = "test test hanlder",
                ParentTheme = parentTheme
            };
            _context.Themes.AddRange(parentTheme, theme);
            _context.SaveChanges();
            var query = new GetThemeHeaderQuery(theme.Id);
            var handler = new GetThemeHeaderHandler(_context);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(theme.Name, result.Name);
            Assert.Equal(theme.Description, result.Desription);
            Assert.Equal(theme.Id, result.Id);
            Assert.NotNull(result.ParentTheme);
            Assert.Equal(parentTheme.Id, result.ParentTheme.Id);
            Assert.Equal(parentTheme.Name, result.ParentTheme.Name);
            Assert.Equal(parentTheme.Description, result.ParentTheme.Desription);
        }

    }
}
