using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.IntegrationTests.Fixtures.Mocks;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class CreateCardHandlerTest : BaseContextTests<LearnEngineContext>
    {
        private const int TestCardPosition = 0;
        public CreateCardHandlerTest(LearningEngineFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task CreateCardHandler_WithValidArguments_ShouldReturnTrue()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");
                dataContainer.CreateCard("testing card question", "testing card answer");

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme, TypeAccess.Write);

                var createCardQuery = new CreateCardCommand(dataContainer.User.Id, dataContainer.Theme.Id,
                                                            dataContainer.Card.Question, dataContainer.Card.Answer);
                var createCardHandler = new CreateCardHandler(context);

                ////Act
                await createCardHandler.Handle(createCardQuery, CancellationToken.None);

                ////Assert
                Assert.NotNull(await context.Cards.FirstOrDefaultAsync(card => card.Question == dataContainer.Card.Question));
                Assert.Equal(dataContainer.Card.Answer, context.Cards.FirstOrDefault
                                                                      (card => card.Question == dataContainer.Card.Question).Answer);
                Assert.Equal(dataContainer.Card.Question, context.Cards.FirstOrDefault
                                                                      (card => card.Question == dataContainer.Card.Question).Question);
            });
        }

        [Fact]
        public async Task CreateCardHandler_WithNonexistentTheme_ShouldReturnException()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");
                dataContainer.CreateCard("testing card question", "testing card answer");

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme, TypeAccess.Write);

                var createCardQuery = new CreateCardCommand(dataContainer.User.Id, -1,
                                                            dataContainer.Card.Question, dataContainer.Card.Answer);
                var createCardHandler = new CreateCardHandler(context);

                ////Act
                Func<Task> createCard = () => createCardHandler.Handle(createCardQuery, CancellationToken.None);
                Exception exception = await Assert.ThrowsAsync<ThemeNotFoundException>(createCard);

                ////Assert
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

            public Card Card { get; set; }

            public void CreateUser(string userName, string email, byte[] password)
            {
                User = new User { Email = email, Password = password, UserName = userName };
            }

            public void CreateTheme(string name, string description)
            {
                Theme = new Theme { Name = name, Description = description };
            }

            public void CreateCard(string question, string answer)
            {
                Card = new Card { Question = question, Answer = answer };
            }
        }
    }
}
