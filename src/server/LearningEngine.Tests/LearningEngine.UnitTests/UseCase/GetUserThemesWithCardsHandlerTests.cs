using LearningEngine.Application.UseCase.Handler;
using LearningEngine.Application.UseCase.Query;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.UnitTests.UseCase
{
    public class GetUserThemesWithCardsHandlerTests
    {
        [Fact]
        public async Task Success()
        {
            //arange
            var td = new TestData(1, 1);
            var mocks = new Mocks();

            //act
            var result = await mocks.TestingHandler.Handle(td.GetThemeFullInfoQuery, CancellationToken.None);

            //Assert
            mocks.MockUow.Verify(_ => _.StartTransaction(), Times.Once);
            mocks.CheckGetThemeHeader(td.GetThemeHeaderQuery.ObjectId, td.GetThemeHeaderQuery.UserId);
            mocks.CheckGetThemeCardsHeader(td.GetThemeCardsQuery.ObjectId, td.GetThemeCardsQuery.ObjectId);
            mocks.CheckGetThemeNotesHeader(td.GetThemeNotesQuery.ObjectId, td.GetThemeNotesQuery.UserId);
            mocks.CheckGetThemeSubThemesHeader(td.GetThemeSubThemesQuery.ObjectId, td.GetThemeSubThemesQuery.UserId);
            mocks.MockUow.Verify(_ => _.CommitTransaction(), Times.Once);
        }

        public class Mocks
        {
            public Mocks()
            {
                MockMediator = new Mock<IMediator>();
                MockUow = new Mock<ITransactionUnitOfWork>();
                MockMediator.Setup(m => m.Send(It.IsAny<GetThemeHeaderQuery>(), CancellationToken.None))
                    .ReturnsAsync(new ThemeDto());

                MockMediator.Setup(m => m.Send(It.IsAny<GetThemeCardsQuery>(), CancellationToken.None))
                    .ReturnsAsync(new List<CardDto>());

                MockMediator.Setup(m => m.Send(It.IsAny<GetThemeNotesQuery>(), CancellationToken.None))
                    .ReturnsAsync(new List<NoteDto>());

                MockMediator.Setup(m => m.Send(It.IsAny<GetThemeSubThemesQuery>(), CancellationToken.None))
                    .ReturnsAsync(new List<ThemeDto>());

                MockUow.Setup(m => m.CommitTransaction());
                MockUow.Setup(m => m.StartTransaction());
                MockUow.Setup(m => m.RollbackTransaction());
                TestingHandler = new GetUserFullInfoHandler(MockMediator.Object, MockUow.Object);
            }

            public void CheckGetThemeHeader(int themeId, int userId)
            {
                MockMediator.Verify(_ => _.Send(It.Is<GetThemeHeaderQuery>
                       (c => c.ObjectId == themeId && c.UserId == userId), CancellationToken.None), Times.Once);
            }

            public void CheckGetThemeCardsHeader(int themeId, int userId)
            {
                MockMediator.Verify(_ => _.Send(It.Is<GetThemeCardsQuery>
                       (c => c.ObjectId == themeId && c.UserId == userId), CancellationToken.None), Times.Once);
            }

            public void CheckGetThemeNotesHeader(int themeId, int userId)
            {
                MockMediator.Verify(_ => _.Send(It.Is<GetThemeNotesQuery>
                       (c => c.ObjectId == themeId && c.UserId == userId), CancellationToken.None), Times.Once);
            }

            public void CheckGetThemeSubThemesHeader(int themeId, int userId)
            {
                MockMediator.Verify(_ => _.Send(It.Is<GetThemeSubThemesQuery>
                       (c => c.ObjectId == themeId && c.UserId == userId), CancellationToken.None), Times.Once);
            }

            public Mock<IMediator> MockMediator { get; set; }
            public Mock<ITransactionUnitOfWork> MockUow { get; set; }
            public GetUserFullInfoHandler TestingHandler { get; set; }
        }

        public class TestData
        {
            public GetThemeFullInfoQuery GetThemeFullInfoQuery { get; set; }
            public GetThemeHeaderQuery GetThemeHeaderQuery { get; set; }
            public GetThemeCardsQuery GetThemeCardsQuery { get; set; }
            public GetThemeNotesQuery GetThemeNotesQuery { get; set; }
            public GetThemeSubThemesQuery GetThemeSubThemesQuery { get; set; }
            public TestData(int userId, int themeId)
            {
                GetThemeFullInfoQuery = new GetThemeFullInfoQuery(userId, themeId);
                GetThemeHeaderQuery = new GetThemeHeaderQuery(themeId, userId);
                GetThemeCardsQuery = new GetThemeCardsQuery(themeId, userId);
                GetThemeNotesQuery = new GetThemeNotesQuery(themeId, userId);
                GetThemeSubThemesQuery = new GetThemeSubThemesQuery(themeId, userId);
            }
        }
    }
}
