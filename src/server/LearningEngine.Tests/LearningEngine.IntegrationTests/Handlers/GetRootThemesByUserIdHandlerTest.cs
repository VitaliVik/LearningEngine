using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Query;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.IntegrationTests.Fixtures.Mocks;
using LearningEngine.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningEngine.Persistence.Handlers;
using Xunit;
using System.Threading;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Constants;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class GetRootThemesByUserIdHandlerTest : BaseContextTests<LearnEngineContext>
    {
        #region fields
        private string userName;
        private string email;
        private string password;
        private List<Theme> themes;
        #endregion
        public GetRootThemesByUserIdHandlerTest(LearningEngineFixture fixture) : base(fixture)
        {
            userName = "Vasyan";
            email = "sobaka@gmail.com";
            password = "krytoiparol";
            themes = new List<Theme> { new Theme { Name = "hoho theme", Description = "nice hoho theme", IsPublic = true},
                                       new Theme { Name = "haha theme", Description = "nice haha theme", IsPublic = true}};
        }

        [Fact]
        public async Task GetRootThemesByUserIdHandler_WithValidArguments_ShouldReturnThemeList()
        {
            await UseContext(async (context) =>
            {
                //Arrange
                var dataCreator = new DataCreator();
                await dataCreator.PushDataIntoDatabase(context, themes, userName, password, email);

                var getRootThemeByUserIdQuery = new GetRootThemesByUserIdQuery(context.Users
                                                                               .FirstOrDefault(user => user.UserName == userName).Id);
                var getRootThemeByUserIdHandler = new GetRootThemesByUserIdHandler(context);

                //Act
                var result = await getRootThemeByUserIdHandler.Handle(getRootThemeByUserIdQuery, CancellationToken.None);

                //Assert
                Assert.Equal(2, result.Count());
                foreach(var theme in themes)
                {
                    Assert.NotNull(result.FirstOrDefault(thm => thm.Name == theme.Name && thm.Desription == theme.Description));
                }
            });
        }

        [Fact]
        public async Task GetRootThemesByUserIdHandler_WithNoRootThemes_ShouldReturnException()
        {
            await UseContext(async (context) =>
            {
                //Arrange
                var dataCreator = new DataCreator();
                await dataCreator.PushDataIntoDatabase(context, new List<Theme>(), userName, password, email);

                var getRootThemeByUserIdQuery = new GetRootThemesByUserIdQuery(context.Users
                                                                               .FirstOrDefault(user => user.UserName == userName).Id);
                var getRootThemeByUserIdHandler = new GetRootThemesByUserIdHandler(context);

                //Act
                var exception = await Assert.ThrowsAsync <Exception>(() => getRootThemeByUserIdHandler.Handle(getRootThemeByUserIdQuery, 
                                                                                                              CancellationToken.None));

                //Assert
                Assert.Equal(CustomConstants.RootThemesNotFount, exception.Message);
            });
        }

        public class DataCreator
        {
            public async Task PushDataIntoDatabase(LearnEngineContext context, List<Theme> themes, 
                                                   string userName, string password, string email)
            {
                var mock = new HasherMocks().HasherMock.Object;
                var user = new User { Email = email, Password = mock.GetHash(password, userName), UserName = userName };
                context.Users.Add(user);

                foreach(var theme in themes)
                {
                    context.Themes.Add(theme);
                }

                await context.SaveChangesAsync();

                foreach(var theme in themes)
                {
                    context.Permissions.Add(new Permission { Access = TypeAccess.Write, ThemeId = theme.Id, UserId = user.Id });
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
