using LearningEngine.Application.UseCase.Command;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Command;
using LearningEngine.Application.UseCase.Handler;
using Xunit;

namespace LearningEngine.UnitTests.UseCase
{
    public class CreateUserThemeUseCaseTest: IClassFixture<CreateUserThemeFixture>
    {
        CreateUserThemeFixture _fixture;
        public CreateUserThemeUseCaseTest(CreateUserThemeFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateUserThemeTest()
        {
        //    var command = new CreateUserThemeCommand("userName", "test", "test", true);
        //    var handler = new CreateUserThemeHandler(_mockMediator.Object, _mockUow.Object);

        //    var result = await handler.Handle(command, CancellationToken.None);

        //    Assert.Equal(default, result);
        //    _mockUow.Verify(m => m.StartTransaction(), Times.Once);
        //    _mockMediator.Verify(m => m.Send(It.Is<GetUserByNameQuery>(q => q.UserName == "userName"), CancellationToken.None),
        //    Times.Once);
        //    _mockMediator.Verify(m => m.Send(It.Is<CreateThemeCommand>(c => c.ThemeName == "test" &&
        //        c.Description == "test" &&
        //        c.IsPublic == true
        //        ), CancellationToken.None), Times.Once);
        //    _mockMediator.Verify(m => m.Send(It.Is<LinkUserToThemeCommand>(c => c.ThemeName == "test" &&
        //        c.UserName == "userName" &&
        //        (c.Access == (Domain.Enum.TypeAccess.Read | Domain.Enum.TypeAccess.Write)
        //        )), CancellationToken.None), Times.Once);
        //    _mockUow.Verify(m => m.CommitTransaction(), Times.Once);
        }
    }
}
