using LearningEngine.Application.Query;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class GetIdentityHandlerTest
    {
        readonly LearnEngineContext _context;
        public GetIdentityHandlerTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }
        [Fact]
        public async Task GetIdentityWhenUserDataCorrect()
        {
            var query = new GetIdentityQuery("somename", "123");
            var handler = new GetIdentityHandler(_context);
            _context.Add(new User { UserName = "somename", Password = "123" });
            _context.SaveChanges();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("somename", "qwerty")]
        [InlineData("dominator1488", "01.12.2008")]
        [InlineData("rolit", "")]
        public async Task GetIndenityWithIncorrectData(string username, string password)
        {
            var query = new GetIdentityQuery(username, password);
            var handler = new GetIdentityHandler(_context);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
