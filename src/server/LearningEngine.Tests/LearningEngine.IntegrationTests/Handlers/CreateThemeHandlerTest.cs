using LearningEngine.Domain.Command;
using LearningEngine.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LearningEngine.Persistence.Handlers;
using System.Threading;
using MediatR;
using LearningEngine.Application.Exceptions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LearningEngine.IntegrationTests.Fixtures;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class CreateThemeHandlerTest : BaseContextTests<LearnEngineContext>
    {
        public CreateThemeHandlerTest(LearningEngineFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task CreateThemeWithCorrectData()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var command = new CreateThemeCommand("someTheme", "just theme", true);
                var handler = new CreateThemeHandler(context);
                var result = await handler.Handle(command, CancellationToken.None);

                ////Act
                var theme = context.Themes.OrderByDescending(thm => thm.Id).FirstOrDefault();

                ////Assert
                Assert.Equal(command.ThemeName, theme.Name);
                Assert.Equal(command.Description, theme.Description);
                Assert.True(theme.IsPublic);
            });
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("themename", null)]
        [InlineData(null, "description")]
        public async Task CreateThemeWithIncorrectData(string themeName, string description)
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var command = new CreateThemeCommand(themeName, description, true);
                var handler = new CreateThemeHandler(context);

                ////Act
                Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

                ////Assert
                await Assert.ThrowsAsync<CreateThemeException>(act);
            });
        }

        [Fact]
        public async Task CreateThemeWithParentTheme()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var parentTheme = new Theme
                {
                    Name = ".net",
                    Description = "all .net themes"
                };
                context.Themes.Add(parentTheme);
                context.SaveChanges();
                var command = new CreateThemeCommand("LINQ", "all about linq", true, parentTheme.Id);
                var handler = new CreateThemeHandler(context);

                ////Act
                await handler.Handle(command, CancellationToken.None);

                ////Assert
                var result = context.Themes
                    .Include(thm => thm.ParentTheme)
                    .OrderByDescending(thm => thm.Id)
                    .FirstOrDefault();

                Assert.NotNull(result);
                Assert.NotNull(result.ParentTheme);
                Assert.Equal(parentTheme.Name, result.ParentTheme.Name);
                Assert.Equal(parentTheme.Description, result.ParentTheme.Description);
            });
        }
    }
}
