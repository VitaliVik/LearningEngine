﻿using LearningEngine.Domain.Query;
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
                context.Themes.AddRange(parentTheme, theme1, theme2);
                context.SaveChanges();
                var query = new GetThemeSubThemesQuery(parentTheme.Id);
                var handler = new GetThemeSubThemesHandler(context);

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
                Assert.NotNull(result.FirstOrDefault(thm => thm.Name == theme1.Name));
                Assert.Equal(theme1.Description, result.FirstOrDefault(thm => thm.Name == theme1.Name).Desription);
                Assert.Equal(theme1.IsPublic, result.FirstOrDefault(thm => thm.Name == theme1.Name).IsPublic);
                Assert.NotNull(result.FirstOrDefault(thm => thm.Name == theme2.Name));
                Assert.Equal(theme2.Description, result.FirstOrDefault(thm => thm.Name == theme2.Name).Desription);
                Assert.Equal(theme2.IsPublic, result.FirstOrDefault(thm => thm.Name == theme2.Name).IsPublic);
            });
        }
    }
}
