using LearningEngine.Domain.Query;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class GetThemeHeaderHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public GetThemeHeaderHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetThemeHeaderTest()
        {
            await UseContext(async (context) =>
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
                context.Themes.AddRange(parentTheme, theme);
                context.SaveChanges();
                var query = new GetThemeHeaderQuery(theme.Id);
                var handler = new GetThemeHeaderHandler(context);

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal(theme.Name, result.Name);
                Assert.Equal(theme.Description, result.Desription);
                Assert.Equal(theme.Id, result.Id);
                Assert.NotNull(result.ParentTheme);
                Assert.Equal(parentTheme.Id, result.ParentTheme.Id);
                Assert.Equal(parentTheme.Name, result.ParentTheme.Name);
                Assert.Equal(parentTheme.Description, result.ParentTheme.Desription);
            });
        }
    }
}
