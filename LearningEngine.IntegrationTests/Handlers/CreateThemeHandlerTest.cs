using LearningEngine.Application.Command;
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
            var correctUser = new User { UserName = "correctUser", Password = "123" };
            _context.Users.Add(correctUser);
            _context.SaveChanges();
            var command = new CreateThemeCommand(correctUser.UserName, "someTheme", "just theme", true);
            var handler = new CreateThemeHandler(_context);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(default, result);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData("rolit", null, null)]
        [InlineData(null, "themename", "desription")]
        public void CreateThemeWithIncorrectData(string username, string themeName, string description)
        {
            var command = new CreateThemeCommand(username, themeName, description, true);
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
            var command = new CreateThemeCommand("rolit", "LINQ", "all about linq", true, parentTheme.Id);
            var handler = new CreateThemeHandler(_context);
            await handler.Handle(command, CancellationToken.None);

            var result = _context.Themes
                .Include(thm => thm.ParentTheme)
                .FirstOrDefault(thm => thm.Name == "LINQ");

            Assert.NotNull(result);
            Assert.NotNull(result.ParentTheme);
        }
    }
}
