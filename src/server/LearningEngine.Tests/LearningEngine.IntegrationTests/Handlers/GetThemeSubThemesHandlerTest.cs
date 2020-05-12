using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Query;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class GetThemeSubThemesHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public GetThemeSubThemesHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetThemeSubThemesTest()
        {
            await UseContext(async (context) =>
            {
                var parentTheme = new Theme
                {
                    Name = "testSubThemesParent",
                    Description = "test Subthemes Parent"
                };
                var theme1 = new Theme
                {
                    Name = "testSubThemes1",
                    Description = "test theme1",
                    ParentTheme = parentTheme
                };
                var theme2 = new Theme
                {
                    Name = "testSubThemes2",
                    Description = "test theme2",
                    ParentTheme = parentTheme
                };
                var user = new User
                {
                    UserName = "fakeName",
                    Password = new byte[0],
                    Email = "fakeemail.gmail.com"
                };
                context.Themes.AddRange(parentTheme, theme1, theme2);
                context.Users.Add(user);
                context.SaveChanges();
                var permission1 = new Permission
                {
                    Access = TypeAccess.Write,
                    UserId = user.Id,
                    ThemeId = parentTheme.Id
                };
                var permission2 = new Permission
                {
                    Access = TypeAccess.Write,
                    UserId = user.Id,
                    ThemeId = theme1.Id
                };
                var permission3 = new Permission
                {
                    Access = TypeAccess.Write,
                    UserId = user.Id,
                    ThemeId = theme2.Id
                };
                context.Permissions.AddRange(permission1, permission2, permission3);
                context.SaveChanges();

                var query = new GetThemeSubThemesQuery(parentTheme.Id, user.Id, TypeAccess.Read);
                var handler = new GetThemeSubThemesHandler(context);

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
                Assert.NotNull(result.FirstOrDefault(thm => thm.Name == theme1.Name));
                Assert.Equal(theme1.Description, result.FirstOrDefault(thm => thm.Name == theme1.Name).Desсription);
                Assert.Equal(theme1.IsPublic, result.FirstOrDefault(thm => thm.Name == theme1.Name).IsPublic);
                Assert.NotNull(result.FirstOrDefault(thm => thm.Name == theme2.Name));
                Assert.Equal(theme2.Description, result.FirstOrDefault(thm => thm.Name == theme2.Name).Desсription);
                Assert.Equal(theme2.IsPublic, result.FirstOrDefault(thm => thm.Name == theme2.Name).IsPublic);
            });
        }
    }
}
