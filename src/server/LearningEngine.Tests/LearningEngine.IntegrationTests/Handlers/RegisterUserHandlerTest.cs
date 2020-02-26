using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using LearningEngine.Persistence.Utils;
using Moq;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class RegisterUserHandlerTest : BaseContextTests<LearnEngineContext>
    {
        private readonly Mock<IPasswordHasher> _mock;
        public RegisterUserHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
            _mock = new Mock<IPasswordHasher>();
            _mock.Setup(m => m.GetHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new byte[64]);
        }


        [Fact]
        public async Task RegisterUserTest()
        {
            await UseContext(async (context) =>
            {
                var handler = new RegisterUserHandler(context, _mock.Object);
                var command = new RegisterUserCommand("username", "email@post.org", "123");

                await handler.Handle(command, CancellationToken.None);

                var user = context.Users.FirstOrDefault(user => user.UserName == "username");
                Assert.NotNull(user);
                Assert.Equal("username", user.UserName);
                Assert.Equal("email@post.org", user.Email);
                Assert.Equal(_mock.Object.GetHash("123", user.UserName), user.Password);
            });
        }

        [Fact]
        public async Task RegisterTwoUserWithTheSameNameOrEmail()
        {
            await UseContext(async (context) =>
            {
                var username = "noname";
                var email = "email@gmail.com";
                var handler = new RegisterUserHandler(context, _mock.Object);
                var command = new RegisterUserCommand(username, email, "123");
                await handler.Handle(command, CancellationToken.None);

                Func<Task> act = () => handler.Handle(command, CancellationToken.None);

                await Assert.ThrowsAsync<RegisterUserException>(act);
            });
        }
    }
}
