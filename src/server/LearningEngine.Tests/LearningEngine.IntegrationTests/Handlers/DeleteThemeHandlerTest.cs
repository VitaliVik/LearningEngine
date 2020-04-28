using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.IntegrationTests.Fixtures.Mocks;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
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
        #region fields
        private string userName;
        private string email;
        private string password;
        private string themeName;
        private string themeDescription;
        #endregion

        public DeleteThemeHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
            userName = "Vasyan";
            email = "sobaka@gmail.com";
            password = "krytoiparol";
            themeName = "test theme";
            themeDescription = "for testing";
        }

        [Fact]
        public async Task DeleteThemeHandler_WithValidArguments_ShouldReturnNull()
        {
            //Arrange
            await UseContext(async (context) =>
            {
                //Arrange
                var dataCreator = new DataCreator();
                await dataCreator.PushDataIntoDatabase(context, userName, email, password, themeName, 
                                                       themeDescription, TypeAccess.Write);

                var deleteThemeCommand = new DeleteThemeCommand(context.Themes.FirstOrDefault(theme => theme.Name == themeName).Id,
                                                                context.Users.FirstOrDefault(user => user.UserName == userName).Id);
                var deleteThemeHandler = new DeleteThemeHandler(context);

                //Act
                await deleteThemeHandler.Handle(deleteThemeCommand, CancellationToken.None);

                //Assert
                Assert.Null(await context.Themes.FirstOrDefaultAsync(theme => theme.Name == themeName));
            });
        }

        [Fact]
        public async Task DeleteThemeHandler_WithInvalidPermission_ShouldReturnException()
        {
            //Arrange
            await UseContext(async (context) =>
            {
                //Arrange
                var dataCreator = new DataCreator();
                await dataCreator.PushDataIntoDatabase(context, userName, email, password, themeName,
                                                       themeDescription, TypeAccess.Read);

                var deleteThemeCommand = new DeleteThemeCommand(context.Themes.FirstOrDefault(theme => theme.Name == themeName).Id,
                                                                context.Users.FirstOrDefault(user => user.UserName == userName).Id);
                var deleteThemeHandler = new DeleteThemeHandler(context);

                //Act
                Exception exception = await Assert.ThrowsAsync<Exception>(() => deleteThemeHandler.Handle(deleteThemeCommand, 
                                                                                                    CancellationToken.None));

                //Assert
                Assert.Equal(CustomConstants.NoRightsForDeleting, exception.Message);
            });
        }

        [Fact]
        public async Task DeleteThemeHandler_WithInvalidTheme_ShouldReturnException()
        {
            //Arrange
            await UseContext(async (context) =>
            {
                //Arrange
                var dataCreator = new DataCreator();
                await dataCreator.PushDataIntoDatabase(context, userName, email, password, themeName,
                                                       themeDescription, TypeAccess.Read);

                var deleteThemeCommand = new DeleteThemeCommand(context.Themes.FirstOrDefault(theme => theme.Name == "dotNet").Id,
                                                                context.Users.FirstOrDefault(user => user.UserName == userName).Id);
                var deleteThemeHandler = new DeleteThemeHandler(context);

                //Act
                Exception exception = await Assert.ThrowsAsync<Exception>(() => deleteThemeHandler.Handle(deleteThemeCommand,
                                                                                                    CancellationToken.None));
                
                //Assert
                Assert.Equal(CustomConstants.ThemeNotFound, exception.Message);
            });
        }

        public class DataCreator
        {
            public async Task PushDataIntoDatabase(LearnEngineContext context, string userName, string email, string password,
                                       string themeName, string themeDescription, TypeAccess userPermission)
            {
                var mock = new HasherMocks().HasherMock.Object;
                var user = new User { Email = email, Password = mock.GetHash(password, userName), UserName = userName };
                var theme = new Theme { Name = themeName, Description = themeDescription, IsPublic = true };
                context.Users.Add(user);
                context.Themes.Add(theme);

                await context.SaveChangesAsync();

                context.Permissions.Add(new Permission { Access = userPermission, ThemeId = theme.Id, UserId = user.Id });

                await context.SaveChangesAsync();
            }
        }
    }
}
