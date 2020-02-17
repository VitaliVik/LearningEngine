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

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class CreateThemeHandlerTest
    {
        readonly LearnEngineContext _context;
        public CreateThemeHandlerTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task CreateThemeWithCorrectData()
        {
            var command = new CreateThemeCommand("someTheme", "just theme", true);
            var handler = new CreateThemeHandler(_context);
            var result = await handler.Handle(command, CancellationToken.None);

            var theme = _context.Themes.OrderByDescending(thm => thm.Id).FirstOrDefault();

            Assert.Equal(command.ThemeName, theme.Name);
            Assert.Equal(command.Description, theme.Description);
            Assert.True(theme.IsPublic);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("themename", null)]
        [InlineData("null", "description")]
        public void CreateThemeWithIncorrectData(string themeName, string description)
        {
            var command = new CreateThemeCommand(themeName, description, true);
            var handler = new CreateThemeHandler(_context);

            Func<Task> act =  () => handler.Handle(command, CancellationToken.None);

            Assert.ThrowsAsync<CreateThemeException>(act);
        }

        [Fact]
        public async Task CreateThemeWithParentTheme()
        {
            var parentTheme = new Theme
            {
                Name = ".net",
                Description = "all .net themes"
            };
            _context.Themes.Add(parentTheme);
            _context.SaveChanges();
            var command = new CreateThemeCommand("LINQ", "all about linq", true, parentTheme.Id);
            var handler = new CreateThemeHandler(_context);
            await handler.Handle(command, CancellationToken.None);

            var result = _context.Themes
                .Include(thm => thm.ParentTheme)
                .OrderByDescending(thm => thm.Id)
                .FirstOrDefault();

            Assert.NotNull(result);
            Assert.NotNull(result.ParentTheme);
            Assert.Equal(parentTheme.Name, result.ParentTheme.Name);
            Assert.Equal(parentTheme.Description, result.ParentTheme.Description);
        }
    }
}
