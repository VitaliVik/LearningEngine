using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
using LearningEngine.IntegrationTests.Fixtures;
using LearningEngine.Persistence.Handlers;
using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Handlers
{
    [Collection("DatabaseCollection")]
    public class EditUserKnowledgeHandlerTests : BaseContextTests<LearnEngineContext>
    {
        private const int KnowledgeValue = 20;
        public EditUserKnowledgeHandlerTests(LearningEngineFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task EditUserKnowledgeHandler_WithValidArguments_ShouldReturnTrue()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");
                dataContainer.CreateCard("testing card question", "testing card answer");
                dataContainer.CreateStatistic(0.0);

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme,
                                    TypeAccess.Write, dataContainer.Card, dataContainer.Statistic);

                var editUserKnowledgeCommand = new EditUserKnowledgeCommand(dataContainer.User.Id,
                                                                            dataContainer.Card.Id,
                                                                            KnowledgeValue);
                var editUserKnowledgeHandler = new EditUserKnowledgeHandler(context);

                ////Act
                await editUserKnowledgeHandler.Handle(editUserKnowledgeCommand, CancellationToken.None);

                ////Assert
                Assert.Equal(KnowledgeValue, context.Statistic.FirstOrDefault
                                             (statistic => statistic.Id == dataContainer.Statistic.Id).CardKnowledge);
            });
        }

        [Fact]
        public async Task EditUserKnowledgeHandler_WithUnexistentCard_ShouldReturnException()
        {
            await UseContext(async (context) =>
            {
                ////Arrange
                var dataContainer = new TestData();
                dataContainer.CreateUser("Vasyan", "sobaka@gmail.com", new byte[0]);
                dataContainer.CreateTheme("test theme", "for testing");
                dataContainer.CreateCard("testing card question", "testing card answer");
                dataContainer.CreateStatistic(0.0);

                new DatabaseFiller(context, dataContainer.User, dataContainer.Theme,
                                    TypeAccess.Write, dataContainer.Card, dataContainer.Statistic);

                var editUserKnowledgeCommand = new EditUserKnowledgeCommand(dataContainer.User.Id,
                                                                            -1,
                                                                            KnowledgeValue);
                var editUserKnowledgeHandler = new EditUserKnowledgeHandler(context);

                ////Act
                Func<Task> editKnowledge = () => editUserKnowledgeHandler.Handle
                                                            (editUserKnowledgeCommand, CancellationToken.None);
                Exception exception = await Assert.ThrowsAsync<CardNotFoundException>(editKnowledge);

                ////Assert
                Assert.Equal(ExceptionDescriptionConstants.CardNotFound, exception.Message);
            });
        }

        public class DatabaseFiller
        {
            public DatabaseFiller(LearnEngineContext context, User user, Theme theme,
                                 TypeAccess userPermission, Card card, Statistic statistic)
            {
                context.Users.Add(user);
                context.Themes.Add(theme);

                context.SaveChanges();

                context.Permissions.Add(new Permission { Access = userPermission, ThemeId = theme.Id, UserId = user.Id });

                card.ThemeId = theme.Id;

                context.Cards.Add(card);

                context.SaveChanges();

                statistic.CardId = card.Id;
                statistic.UserId = user.Id;

                context.Statistic.Add(statistic);

                context.SaveChanges();
            }

        }

        public class TestData
        {
            public User User { get; set; }

            public Theme Theme { get; set; }

            public Card Card { get; set; }

            public Statistic Statistic { get; set; }

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
            public void CreateStatistic(double cardKnowledge)
            {
                Statistic = new Statistic { CardKnowledge = cardKnowledge };
            }
        }
    }
}
