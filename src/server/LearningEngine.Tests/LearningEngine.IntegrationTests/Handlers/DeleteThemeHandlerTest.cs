using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.IntegrationTests.Fixtures.Mocks;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class DeleteThemeHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public DeleteThemeHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task DeleteThemeHandler_WithValidArguments_ShouldReturnNull()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme, TypeAccess.Write);

                var deleteThemeCommand = new DeleteThemeCommand
                                (context.Themes.FirstOrDefault(theme => theme.Name == dataContainer.Theme.Name).Id,
                                context.Users.FirstOrDefault(user => user.UserName == dataContainer.User.UserName).Id);
                var deleteThemeHandler = new DeleteThemeHandler(context);
                
                ////Act
                await deleteThemeHandler.Handle(deleteThemeCommand, CancellationToken.None);

                ////Assert
                Assert.Null(await context.Themes.FirstOrDefaultAsync(theme => theme.Name == dataContainer.Theme.Name));
            });
        }

        [Fact]
        public async Task DeleteThemeHandler_WithInvalidTheme_ShouldReturnException()
        {
            await UseContext(async (context) =>
            {
                //Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme, TypeAccess.Read);

                var deleteThemeCommand = new DeleteThemeCommand
                                              (-1,
                                              context.Users.FirstOrDefault
                                              (user => user.UserName == dataContainer.User.UserName).Id);
                var deleteThemeHandler = new DeleteThemeHandler(context);

                //Act
                Func<Task> deleteTheme = () => deleteThemeHandler.Handle(deleteThemeCommand, CancellationToken.None);
                Exception exception = await Assert.ThrowsAsync<ThemeNotFoundException>(deleteTheme);

                //Assert
                Assert.Equal(ExceptionDescriptionConstants.ThemeNotFound, exception.Message);
            });
        }

        public class DatabaseFiller
        {
            public DatabaseFiller(LearnEngineContext context, User user, Theme theme, TypeAccess userPermission)
            {
                var mock = new HasherMocks().HasherMock.Object;
                context.Users.Add(user);
                context.Themes.Add(theme);

                context.SaveChanges();

                context.Permissions.Add(new Permission { Access = userPermission, ThemeId = theme.Id, UserId = user.Id });

                context.SaveChanges();
            }

        }

        public class TestData
        {
            public User User { get; set; }

            public Theme Theme { get; set; }

            public void CreateUser(string userName, string email, byte[] password)
            {
                User = new User { Email = email, Password = password, UserName = userName };
            }
            public void CreateTheme(string name, string description)
            {
                Theme = new Theme { Name = name, Description = description };
            }
        }
    }
}
