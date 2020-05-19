using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Query;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.IntegrationTests.Fixtures.Mocks;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class CheckUserPermissionsHandlerTests : BaseContextTests<LearnEngineContext>
    {
        public CheckUserPermissionsHandlerTests(LearningEngineFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task CheckUserPermissionsHandler_WithValidArguments_ShouldReturnTrue()
        {
            await UseContext(async (context) =>
            {
                //Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");
                dataContainer.CreateTypeAccess(TypeAccess.Write);

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme, dataContainer.Access);

                var checkUserPermissionQuery = new CheckUserPermissionsQuery(dataContainer.User.Id,
                                                                             dataContainer.Theme.Id,
                                                                             dataContainer.Access);
                var chechUserPermissionHandler = new CheckUserPermissionsHandler(context);

                //Act
                var hasPermissions = chechUserPermissionHandler.Handle(checkUserPermissionQuery, 
                                                                       CancellationToken.None);
                //Assert
                Assert.Equal(default, hasPermissions.Result);
            });
        }

        [Fact]
        public async Task CheckUserPermissionsHandler_WithInvalidPermissions_ShouldReturnException()
        {
            await UseContext(async (context) =>
            {
                //Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");
                dataContainer.CreateTypeAccess(TypeAccess.Write);

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme, dataContainer.Access);

                var checkUserPermissionQuery = new CheckUserPermissionsQuery(dataContainer.User.Id,
                                                                             dataContainer.Theme.Id,
                                                                             TypeAccess.Read);
                var chechUserPermissionHandler = new CheckUserPermissionsHandler(context);

                //Act
                Func<Task> checkPermissions = () => chechUserPermissionHandler.Handle
                                                    (checkUserPermissionQuery, CancellationToken.None);
                var exception = await Assert.ThrowsAsync<Exception>(checkPermissions);

                //Assert
                Assert.Equal(ExceptionDescriptionConstants.NoPermissions, exception.Message);
            });
        }

        public class DatabaseFiller
        {
            public DatabaseFiller(LearnEngineContext context, User user, Theme theme, TypeAccess userPermission)
            {
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
            public TypeAccess Access { get; set; }
            public void CreateUser(string userName, string email, byte[] password)
            {
                User = new User { Email = email, Password = password, UserName = userName };
            }
            public void CreateTheme(string name, string description)
            {
                Theme = new Theme { Name = name, Description = description };
            }
            public void CreateTypeAccess(TypeAccess access)
            {
                Access = access;
            }
        }
    }
}
