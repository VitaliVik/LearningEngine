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
using LearningEngine.Application.Exceptions;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class GetRootThemesByUserIdHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public GetRootThemesByUserIdHandlerTest(LearningEngineFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetRootThemesByUserIdHandler_WithValidArguments_ShouldReturnThemeList()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateThemes(new List<Theme>
                                        { new Theme { Name = "hoho", Description = "hoho desc" },
                                          new Theme { Name = "hoho", Description = "hoho desc" }});

                new DatabaseFiller(context, dataContainer.Themes, dataContainer.User);

                var getRootThemeByUserIdQuery = new GetRootThemesByUserIdQuery(context.Users
                                       .FirstOrDefault(user => user.UserName == dataContainer.User.UserName).Id);
                var getRootThemeByUserIdHandler = new GetRootThemesByUserIdHandler(context);

                ////Act
                var result = await getRootThemeByUserIdHandler.Handle(getRootThemeByUserIdQuery, CancellationToken.None);

                ////Assert
                Assert.Equal(dataContainer.Themes.Count(), result.Count());
                foreach (var theme in dataContainer.Themes)
                {
                    Assert.NotNull(result.FirstOrDefault(thm => thm.Name == theme.Name));
                }
            });
        }

        [Fact]
        public async Task GetRootThemesByUserIdHandler_WithNoRootThemes_ShouldReturnException()
        {
            await UseContext(async (context) =>
            {
                //Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateThemes(new List<Theme> { });

                new DatabaseFiller(context, dataContainer.Themes, dataContainer.User);

                var getRootThemeByUserIdQuery = new GetRootThemesByUserIdQuery(context.Users
                                       .FirstOrDefault(user => user.UserName == dataContainer.User.UserName).Id);
                var getRootThemeByUserIdHandler = new GetRootThemesByUserIdHandler(context);

                //Act

                Func<Task> getRootTheme = () => getRootThemeByUserIdHandler.Handle
                                                            (getRootThemeByUserIdQuery, CancellationToken.None);
                var exception = await Assert.ThrowsAsync<RootThemesNotFoundException>(getRootTheme);

                //Assert
                Assert.Equal(ExceptionDescriptionConstants.RootThemesNotFount, exception.Message);
            });
        }

        public class DatabaseFiller
        {
            public DatabaseFiller(LearnEngineContext context, List<Theme> themes, User user)
            {
                var mock = new HasherMocks().HasherMock.Object;
                context.Users.Add(user);

                foreach (var theme in themes)
                {
                    context.Themes.Add(theme);
                }

                context.SaveChanges();

                foreach (var theme in themes)
                {
                    context.Permissions.Add(new Permission { Access = TypeAccess.Write, ThemeId = theme.Id, UserId = user.Id });
                }

                context.SaveChanges();
            }
        }

        public class TestData
        {
            public User User { get; set; }

            public List<Theme> Themes { get; set; }

            public void CreateUser(string userName, string email, byte[] password)
            {
                User = new User { Email = email, Password = password, UserName = userName };
            }
            public void CreateThemes(List<Theme> themes)
            {
                Themes = new List<Theme>();
                foreach (var theme in themes)
                {
                    Themes.Add(theme);
                }
            }
        }
    }
}
