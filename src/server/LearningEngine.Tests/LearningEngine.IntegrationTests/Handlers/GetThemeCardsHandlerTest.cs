using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Query;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class GetThemeCardsHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public GetThemeCardsHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetThemeCardsTest()
        {
            await UseContext(async (context) =>
            {
                var theme = new Theme
                {
                    Name = "themeCardsTests",
                    Description = "theme cards tests"
                };
                var cards = new List<Card>
                {
                    new Card { Theme = theme, Question = "1", Answer = "first"},
                    new Card { Theme = theme, Question = "2", Answer = "second"},
                };
                var user = new User
                {
                    UserName = "testUser",
                    Email = "test@gmail.com",
                    Password = new byte[0]
                };
                context.Themes.Add(theme);
                context.Cards.AddRange(cards);
                context.Users.Add(user);
                context.SaveChanges();
                var permissions = new Permission
                {
                    UserId = user.Id,
                    ThemeId = theme.Id,
                    Access = TypeAccess.Read
                };
                context.Permissions.Add(permissions);
                context.SaveChanges();
                var query = new GetThemeCardsQuery(theme.Id, user.Id);
                var handler = new GetThemeCardsHandler(context);

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal(cards.Count, result.Count);
                Assert.Equal(cards.FirstOrDefault(card => card.Question == "1").Answer,
                    result.FirstOrDefault(card => card.Question == "1").Answer);
                Assert.Equal(cards.FirstOrDefault(card => card.Question == "2").Answer,
                    result.FirstOrDefault(card => card.Question == "2").Answer);
            });
        }
    }
}
