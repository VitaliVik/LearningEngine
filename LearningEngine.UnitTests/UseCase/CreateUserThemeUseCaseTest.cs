using LearningEngine.Application.UseCase.Command;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Command;
using LearningEngine.Application.UseCase.Handler;
using Xunit;
using LearningEngine.Persistence.Models;
using LearningEngine.Domain.Enum;

namespace LearningEngine.UnitTests.UseCase
{
    public class CreateUserThemeUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            //arange
            var td = new TestData("rolit", "test");
            var mocks = new Mocks(td);

            //act
            var result = await mocks.TestingHandler.Handle(td.CreateUserThemeCommand, CancellationToken.None);

            //assert
            mocks.MockUow.Verify(_ => _.StartTransaction(), Times.Once);
            mocks.MockMediator.Verify(_ => _.Send(It.Is<GetUserByNameQuery>(
                c => c.UserName == td.GetUserByNameQuery.UserName), CancellationToken.None), Times.Once);
            mocks.MockMediator.Verify(_ => _.Send(It.Is<CreateThemeCommand>(
                c => c.ThemeName == td.CreateThemeCommand.ThemeName), CancellationToken.None), Times.Once);
            mocks.MockMediator.Verify(_ => _.Send(It.IsAny<LinkUserToThemeCommand>(), CancellationToken.None), Times.Once);
            mocks.MockUow.Verify(_ => _.CommitTransaction(), Times.Once);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task ThrowExceptionUserNotFound(string userName)
        {
            //arange
            var td = new TestData(userName, "test");
            var mocks = new Mocks(td);

            //act
            Func<Task<Unit>> act =  () => mocks.TestingHandler.Handle(td.CreateUserThemeCommand, CancellationToken.None);

            //assert
            await Assert.ThrowsAsync<Exception>(act);
            mocks.MockUow.Verify(_ => _.StartTransaction(), Times.Once);
            mocks.MockUow.Verify(_ => _.RollbackTransaction(), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]

        public async Task ThrowExceptionIncorrectTheme(string themeName)
        {
            //arange
            var td = new TestData("user", themeName);
            var mocks = new Mocks(td);

            //act
            Func<Task<Unit>> act = () => mocks.TestingHandler.Handle(td.CreateUserThemeCommand, CancellationToken.None);

            //assert
            await Assert.ThrowsAsync<Exception>(act);
            mocks.MockUow.Verify(_ => _.StartTransaction(), Times.Once);
            mocks.MockUow.Verify(_ => _.RollbackTransaction(), Times.Once);
        }

        

        public class Mocks
        {
            public Mocks(TestData td)
            {
                MockMediator = new Mock<IMediator>();
                MockUow = new Mock<ITransactionUnitOfWork>();
                MockMediator.Setup(m => m.Send(It.Is<GetUserByNameQuery>(q => q.UserName == "" || q.UserName == null), CancellationToken.None))
                    .ReturnsAsync((UserDto)null);
                MockMediator.Setup(m => m.Send(
                    It.Is<GetUserByNameQuery>(q => td.User.UserName == q.UserName && (td.User.UserName != null && td.User.UserName != "")), CancellationToken.None))
                    .ReturnsAsync(new UserDto { UserName = td.User.UserName });

                MockMediator.Setup(m => m.Send(It.IsAny<CreateThemeCommand>(), CancellationToken.None))
                    .ReturnsAsync(new Unit());
                MockMediator.Setup(m => m.Send(It.Is<CreateThemeCommand>(c => c.ThemeName == null || c.ThemeName == ""), CancellationToken.None))
                    .Throws<Exception>();

                MockMediator.Setup(m => m.Send(It.IsAny<LinkUserToThemeCommand>(), CancellationToken.None))
                    .ReturnsAsync(new Unit());

                MockUow.Setup(m => m.CommitTransaction());
                MockUow.Setup(m => m.StartTransaction());
                MockUow.Setup(m => m.RollbackTransaction());
                TestingHandler = new CreateUserThemeHandler(MockMediator.Object, MockUow.Object);
            }
            public Mock<IMediator> MockMediator { get; set; }
            public Mock<ITransactionUnitOfWork> MockUow { get; set; }
            public CreateUserThemeHandler TestingHandler { get; set; }

        }
        public class TestData
        {
            public TestData(string userName, string themeName)
            {
                User = new User { UserName = userName };
                CreateUserThemeCommand = new CreateUserThemeCommand(
                    User.UserName,
                    themeName,
                    "TestDescription",
                    true);
                GetUserByNameQuery = new GetUserByNameQuery(CreateUserThemeCommand.UserName);
                CreateThemeCommand = new CreateThemeCommand(
                    CreateUserThemeCommand.ThemeName,
                    CreateUserThemeCommand.Description,
                    CreateUserThemeCommand.IsPublic);
                LinkUserToTheme = new LinkUserToThemeCommand(
                    User.UserName,
                    CreateThemeCommand.ThemeName,
                    TypeAccess.Read | TypeAccess.Write);
            }
            public User User { get; set; }
            public CreateUserThemeCommand CreateUserThemeCommand { get; set; }
            public GetUserByNameQuery GetUserByNameQuery { get; set; }
            public CreateThemeCommand CreateThemeCommand { get; set; }
            public LinkUserToThemeCommand LinkUserToTheme { get; set; }

        }
    }
}
