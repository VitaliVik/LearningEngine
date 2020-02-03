using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using LearningEngine.Application.Command;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Api.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Data.SqlClient;
using LearningEngine.Application.Exceptions;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class RegisterUserHandlerTest
    {
        private readonly LearnEngineContext _context;

        public RegisterUserHandlerTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }


        [Fact]
        public async Task RegisterUserTest()
        {
            var handler = new RegisterUserHandler(_context);
            var command = new RegisterUserCommand("username", "email@post.org", "123");

            await handler.Handle(command, CancellationToken.None);

            var user = _context.Users.FirstOrDefault(user => user.UserName == "username");
            Assert.NotNull(user);
        }

        [Fact]
        public async Task RegisterTwoUserWithTheSameNameOrEmail()
        {
            var username = "noname";
            var email = "email@gmail.com";
            var handler = new RegisterUserHandler(_context);
            var command = new RegisterUserCommand(username, email, "123");
            await handler.Handle(command, CancellationToken.None);
            
            Func<Task> act = () => handler.Handle(command, CancellationToken.None);
            
            await Assert.ThrowsAsync<RegisterUserException>(act);
        }
    }
}
