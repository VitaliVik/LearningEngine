using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.IntegrationTests.Fixtures.Mocks;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class EditThemeHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public EditThemeHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task EditThemeHandler_WithValidArguments_ShouldReturnTrue()
        {
            await UseContext(async (context) =>
            {
                //Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing", true);

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme, TypeAccess.Write);

                var editThemeCommand = new EditThemeCommand(
                    new ThemeDto
                    {
                        Id = dataContainer.Theme.Id,
                        Desсription = dataContainer.Theme.Description,
                        Name = dataContainer.Theme.Name,
                        IsPublic = dataContainer.Theme.IsPublic
                    },
                    dataContainer.User.Id,
                    dataContainer.Theme.Id,
                    TypeAccess.Write);
                var handler = new EditThemeHandler(context);

                //Act
                await handler.Handle(editThemeCommand, CancellationToken.None);

                var editedTheme = await context.Themes.FirstOrDefaultAsync(theme => theme.Id == dataContainer.Theme.Id);

                //Assert
                Assert.Equal(dataContainer.Theme.Description, editedTheme.Description);
                Assert.Equal(dataContainer.Theme.Name, editedTheme.Name);
                Assert.Equal(dataContainer.Theme.IsPublic, editedTheme.IsPublic);
            });
        }

        [Fact]
        public async Task EditThemeHandler_WithNonexistentTheme_ShouldReturnException()
        {
            await UseContext(async (context) =>
            {
                //Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing", true);

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme, TypeAccess.Read);

                var editThemeCommand = new EditThemeCommand(
                    new ThemeDto
                    {
                        Id = -1,
                        Desсription = "wrong theme desсription",
                        Name = "wrong theme",
                        IsPublic = dataContainer.Theme.IsPublic
                    },
                    dataContainer.User.Id,
                    -1,
                    TypeAccess.Write);
                var handler = new EditThemeHandler(context);

                //Act
                Func<Task> editTheme = () => handler.Handle(editThemeCommand, CancellationToken.None);
                Exception exception = await Assert.ThrowsAsync<Exception>(editTheme);

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
            public void CreateTheme(string name, string description, bool isPublic)
            {
                Theme = new Theme { Name = name, Description = description, IsPublic = isPublic };
            }
        }
    }
}
