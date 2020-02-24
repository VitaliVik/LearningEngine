using LearningEngine.Domain.Query;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using LearningEngine.Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class GetIdentityHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public GetIdentityHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetIdentityWhenUserDataCorrect()
        {
            await UseContext(async (context) =>
            {
                var query = new GetIdentityQuery("somename", "123");
                var handler = new GetIdentityHandler(context);
                context.Add(new User { UserName = "somename", Password = PasswordHasher.GetHash("123","somename") });
                context.SaveChanges();

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal(2, result.Claims.Count());
                Assert.Equal("somename", result.Claims.FirstOrDefault(clm => clm.Type == ClaimsIdentity.DefaultNameClaimType).Value);
                Assert.Equal("user", result.Claims.FirstOrDefault(clm => clm.Type == ClaimsIdentity.DefaultRoleClaimType).Value);

            });
        }

        [Theory]
        [InlineData("somename", "qwerty")]
        [InlineData("dominator1488", "01.12.2008")]
        [InlineData("rolit", "")]
        public async Task GetIndenityWithIncorrectData(string username, string password)
        {
            await UseContext(async (context) =>
            {
                var query = new GetIdentityQuery(username, password);
                var handler = new GetIdentityHandler(context);

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.Null(result);

            });
        }
    }
}
