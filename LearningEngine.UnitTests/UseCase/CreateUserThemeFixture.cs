using LearningEngine.Domain.Command;
using LearningEngine.Domain.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using LearningEngine.Domain.Enum;
using MediatR;
using LearningEngine.Domain.Interfaces;
using System.Threading;
using LearningEngine.Domain.DTO;
using System.Linq;
using Xunit;

namespace LearningEngine.UnitTests.UseCase
{
    public class CreateUserThemeFixture
    {
        public CreateUserThemeFixture()
        {
            TestData = new CreateUserThemeTestData();
            Mocks = new CreateUserThemeMocks(TestData);
        }
        public CreateUserThemeMocks Mocks { get; set; }
        public CreateUserThemeTestData TestData { get; set; }

        public class CreateUserThemeMocks
        {
            public CreateUserThemeMocks(CreateUserThemeTestData data)
            {
                MockMediator = new Mock<IMediator>();
                MockUow = new Mock<ITransactionUnitOfWork>();
                MockMediator.Setup(m => m.Send(It.Is<GetUserByNameQuery>(q => data.GetUserByNameQueryСorrect.UserName == q.UserName), CancellationToken.None))
                    .ReturnsAsync(new UserDto { UserName = "rolit", Email = "rolit@mail.com" });
                MockMediator.Setup(m => m.Send(It.Is<CreateThemeCommand>(c => 
                    data.CreateThemeCommandList.Any(dq => dq.ThemeName == c.ThemeName)
                        ), CancellationToken.None))
                    .ReturnsAsync(new Unit());
                MockMediator.Setup(m => m.Send(It.Is<LinkUserToThemeCommand>(c => data.LinkCommandList.Any(dq =>
                    c.ThemeName == dq.ThemeName &&
                    c.UserName == dq.UserName &&
                    (c.Access == dq.Access))), CancellationToken.None))
                    .ReturnsAsync(new Unit());
                MockMediator.Setup(m => m.Send(It.Is<GetUserByNameQuery>(q => q.UserName == null || q.UserName == ""), CancellationToken.None))
                    .ReturnsAsync((UserDto)null);
                MockMediator.Setup(m => m.Send(It.Is<CreateThemeCommand>(c => c.ThemeName == null || c.ThemeName == ""), CancellationToken.None))
                    .Throws(new Exception);
                MockUow.Setup(m => m.CommitTransaction());
                MockUow.Setup(m => m.StartTransaction());
                MockUow.Setup(m => m.RollbackTransaction());
            }
            public Mock<IMediator> MockMediator { get; set; }
            public Mock<ITransactionUnitOfWork> MockUow { get; set; }
        }
        public class CreateUserThemeTestData
        {
            public GetUserByNameQuery GetUserByNameQueryСorrect{ get; set; } = new GetUserByNameQuery("rolit");
            public List<GetUserByNameQuery> GetUserByNameQueryIncorrect { get; set; } = new List<GetUserByNameQuery>
            {
                new GetUserByNameQuery(null),
                new GetUserByNameQuery("")
            };
            public List<CreateThemeCommand> CreateThemeCommandList { get; set; } = new List<CreateThemeCommand>
            {
                new CreateThemeCommand("theme1", "description", true),
                new CreateThemeCommand("theme2", null, true),
                new CreateThemeCommand(null, null, true),
            };
            public List<LinkUserToThemeCommand> LinkCommandList { get; set; } = new List<LinkUserToThemeCommand>
            {
                new LinkUserToThemeCommand("rolit", "theme1", TypeAccess.Read | TypeAccess.Write),
                new LinkUserToThemeCommand("rolit", "theme2", TypeAccess.Read | TypeAccess.Write),
            };
        }
    }
}
