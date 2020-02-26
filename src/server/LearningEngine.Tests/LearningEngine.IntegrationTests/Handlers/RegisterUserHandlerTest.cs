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
using LearningEngine.IntegrationTests.Fixtures.Mocks;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class RegisterUserHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public RegisterUserHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
        }


        [Fact]
        public async Task RegisterUserTest()
        {
            await UseContext(async (context) =>
            {
                var _mock = new HasherMocks().HasherMock;
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
                var _mock = new HasherMocks().HasherMock;
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
