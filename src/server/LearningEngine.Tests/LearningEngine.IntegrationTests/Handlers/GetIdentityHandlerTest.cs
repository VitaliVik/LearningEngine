using LearningEngine.Domain.Query;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using LearningEngine.Persistence.Utils;
using Moq;
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
        private readonly Mock<IPasswordHasher> _mock;
        public GetIdentityHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
            _mock = new Mock<IPasswordHasher>();
            _mock.Setup(m => m.GetHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new byte[64]);
        }

        [Fact]
        public async Task GetIdentityWhenUserDataCorrect()
        {
            await UseContext(async (context) =>
            {
                var query = new GetIdentityQuery("somename", "123");
                var handler = new GetIdentityHandler(context, _mock.Object);
                context.Add(new User { UserName = "somename", Password = _mock.Object.GetHash("123", "somename") });
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
        [InlineData("rolit", "4124124234")]
        public async Task GetIndenityWithIncorrectData(string username, string password)
        {
            await UseContext(async (context) =>
            {
                var query = new GetIdentityQuery(username, password);
                var handler = new GetIdentityHandler(context, _mock.Object);

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.Null(result);

            });
        }
    }
}
