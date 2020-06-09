using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class CreateStatisicHandlerTests : BaseContextTests<LearnEngineContext>
    {
        public CreateStatisicHandlerTests(LearningEngineFixture fixture) : base(fixture)
        {

        }

        [Fact]
        public async Task CreateStatisicHandler_WithValidArguments_ShouldReturnTrue()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");
                dataContainer.CreateCard("testing card question", "testing card answer");

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme,
                                    TypeAccess.Write, dataContainer.Card);

                var createStatisticCommand = new CreateStatisicCommand(dataContainer.User.Id,
                                                                       dataContainer.Card.Id);
                var createStatisticHandler = new CreateStatisicHandler(context);

                ////Act
                await createStatisticHandler.Handle(createStatisticCommand, CancellationToken.None);

                ////Assert
                Assert.NotNull(await context.Statistic.FirstOrDefaultAsync
                                                       (statistic => statistic.CardId == dataContainer.Card.Id &&
                                                                     statistic.UserId == dataContainer.User.Id));
            });
        }

        [Fact]
        public async Task CreateStatisicHandler_WithInUnexistentCard_ShouldReturnException()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");
                dataContainer.CreateCard("testing card question", "testing card answer");

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme,
                                    TypeAccess.Write, dataContainer.Card);

                var createStatisticCommand = new CreateStatisicCommand(dataContainer.User.Id,
                                                                       -1);
                var createStatisticHandler = new CreateStatisicHandler(context);

                ////Act
                Func<Task> createStatistic = () => createStatisticHandler.Handle
                                                    (createStatisticCommand, CancellationToken.None);
                var exception = await Assert.ThrowsAsync<CardNotFoundException>(createStatistic);

                //Assert
                Assert.Equal(ExceptionDescriptionConstants.CardNotFound, exception.Message);
            });
        }

        public class DatabaseFiller
        {
            public DatabaseFiller(LearnEngineContext context, User user, Theme theme,
                                 TypeAccess userPermission, Card card)
            {
                context.Users.Add(user);
                context.Themes.Add(theme);

                context.SaveChanges();

                context.Permissions.Add(new Permission { Access = userPermission, ThemeId = theme.Id, UserId = user.Id });

                card.ThemeId = theme.Id;

                context.Cards.Add(card);

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
