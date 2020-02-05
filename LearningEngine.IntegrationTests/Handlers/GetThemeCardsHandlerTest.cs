using LearningEngine.Domain.Query;
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
    public class GetThemeCardsHandlerTest
    {
        private readonly LearnEngineContext _context;
        public GetThemeCardsHandlerTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetThemeCardsTest()
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
            _context.Themes.Add(theme);
            _context.Cards.AddRange(cards);
            _context.SaveChanges();
            var query = new GetThemeCardsQuery(theme.Id);
            var handler = new GetThemeCardsHandler(_context);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(cards.Count, result.Count);
            Assert.Equal(cards.FirstOrDefault(card => card.Question == "1").Answer,
                result.FirstOrDefault(card => card.Question == "1").Answer);
            Assert.Equal(cards.FirstOrDefault(card => card.Question == "2").Answer,
                result.FirstOrDefault(card => card.Question == "2").Answer);

        }
        
    }
}
