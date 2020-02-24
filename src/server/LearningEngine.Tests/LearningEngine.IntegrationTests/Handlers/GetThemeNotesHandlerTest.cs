using LearningEngine.Domain.Query;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class GetThemeNotesHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public GetThemeNotesHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetThemeNotesTest()
        {
            await UseContext(async (context) =>
            {
                var theme = new Theme
                {
                    Name = "themeNotesTests",
                    Description = "theme notes tests"
                };
                var notes = new List<Note>
                {
                    new Note { Theme = theme, Title = "1", Content = "first"},
                    new Note { Theme = theme, Title = "2", Content = "second"},
                };
                context.Themes.Add(theme);
                context.Notes.AddRange(notes);
                context.SaveChanges();
                var query = new GetThemeNotesQuery(theme.Id);
                var handler = new GetThemeNotesHandler(context);

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal(notes.Count, result.Count);
                Assert.Equal(notes.FirstOrDefault(note => note.Title == "1").Content,
                    result.FirstOrDefault(note => note.Title == "1").Content);
                Assert.Equal(notes.FirstOrDefault(note => note.Title == "2").Content,
                    result.FirstOrDefault(note => note.Title == "2").Content);
            });
        }
    }
}
