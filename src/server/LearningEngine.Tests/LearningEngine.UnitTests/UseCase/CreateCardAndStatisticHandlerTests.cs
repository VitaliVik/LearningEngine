using LearningEngine.Application.UseCase.Command;
using LearningEngine.Application.UseCase.Handler;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Persistence.Models;
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
    public class CreateCardAndStatisticHandlerTests
    {

        [Fact]
        public async Task Success()
        {
            //arange
            var td = new TestData(1, 1, "question", "answer");
            var mocks = new Mocks();

            //act
            var result = await mocks.TestingHandler.Handle(td.CreateCardAndStatisticCommand, 
                                                                    CancellationToken.None);

            //Assert
            mocks.MockUow.Verify(_ => _.StartTransaction(), Times.Once);
            mocks.MockMediator.Verify(_ => _.Send(It.IsAny<CreateCardCommand>(), 
                                                CancellationToken.None), Times.Once);
            mocks.MockMediator.Verify(_ => _.Send(It.IsAny<CreateStatisicCommand>(), 
                                                CancellationToken.None), Times.Once);
            mocks.MockUow.Verify(_ => _.CommitTransaction(), Times.Once);
        }

        public class Mocks
        {
            public Mocks()
            {
                MockMediator = new Mock<IMediator>();
                MockUow = new Mock<ITransactionUnitOfWork>();
                MockMediator.Setup(m => m.Send(It.IsAny<CreateCardCommand>(), CancellationToken.None))
                    .ReturnsAsync(new int());

                MockMediator.Setup(m => m.Send(It.IsAny<CreateStatisicCommand>(), CancellationToken.None))
                    .ReturnsAsync(new Unit());

                MockUow.Setup(m => m.CommitTransaction());
                MockUow.Setup(m => m.StartTransaction());
                MockUow.Setup(m => m.RollbackTransaction());
                TestingHandler = new CreateCardAndStatisticHandler(MockMediator.Object, MockUow.Object);
            }
            public Mock<IMediator> MockMediator { get; set; }
            public Mock<ITransactionUnitOfWork> MockUow { get; set; }
            public CreateCardAndStatisticHandler TestingHandler { get; set; }

        }

        public class TestData
        {
            public CreateCardAndStatisticCommand CreateCardAndStatisticCommand { get; set; }
            public CreateCardCommand CreateCardCommand { get; set; }
            public CreateStatisicCommand CreateStatisicCommand { get; set; }
            public TestData(int userId, int themeId, string quertion, string answer)
            {
                CreateCardAndStatisticCommand = new CreateCardAndStatisticCommand
                                                    (userId, themeId, quertion, answer);
                CreateCardCommand = new CreateCardCommand(userId, themeId, quertion, answer);
                CreateStatisicCommand = new CreateStatisicCommand(userId, 1);
            }
        }
            
    }
}
