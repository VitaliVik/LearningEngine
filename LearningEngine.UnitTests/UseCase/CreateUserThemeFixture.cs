//using LearningEngine.Domain.Command;
//using LearningEngine.Domain.Query;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using LearningEngine.Domain.Enum;
//using MediatR;
//using LearningEngine.Domain.Interfaces;
//using System.Threading;
//using LearningEngine.Domain.DTO;
//using System.Linq;
//using Xunit;
//using LearningEngine.Application.UseCase.Command;
//using LearningEngine.Persistence.Models;

//namespace LearningEngine.UnitTests.UseCase
//{
//    public class CreateUserThemeFixture
//    {
//        public CreateUserThemeFixture()
//        {
//        }

        

//        public class Mocks
//        {
//            public Mocks(TestData td)
//            {
//                MockMediator = new Mock<IMediator>();
//                MockUow = new Mock<ITransactionUnitOfWork>();
//                MockMediator.Setup(m => m.Send(It.Is<GetUserByNameQuery>(q => q.UserName == "" || q.UserName == null), CancellationToken.None))
//                    .Throws<Exception>();
//                MockMediator.Setup(m => m.Send(It.Is<GetUserByNameQuery>(q => td.User.UserName == q.UserName), CancellationToken.None))
//                    .ReturnsAsync(new UserDto { UserName = td.User.UserName});

//                MockMediator.Setup(m => m.Send(It.Is<CreateThemeCommand>(c =>  
//                td.CreateThemeCommand.ThemeName == c.ThemeName), CancellationToken.None))
//                    .ReturnsAsync(new Unit());
//                MockMediator.Setup(m => m.Send(It.Is<CreateThemeCommand>(c => c.ThemeName == null || c.ThemeName == ""), CancellationToken.None))
//                    .Throws<Exception>();

//                MockMediator.Setup(m => m.Send(It.Is<LinkUserToThemeCommand>(
//                    c => c.ThemeName == td.CreateThemeCommand.ThemeName && c.UserName == td.User.UserName), CancellationToken.None))
//                    .ReturnsAsync(new Unit());

//                MockMediator.Setup(m => m.Send(It.Is<GetUserByNameQuery>(q => q.UserName == null || q.UserName == ""), CancellationToken.None))
//                    .ReturnsAsync((UserDto)null);
//                MockMediator.Setup(m => m.Send(It.Is<CreateThemeCommand>(c => c.ThemeName == null || c.ThemeName == ""), CancellationToken.None))
//                    .Throws(new Exception);
//                MockUow.Setup(m => m.CommitTransaction());
//                MockUow.Setup(m => m.StartTransaction());
//                MockUow.Setup(m => m.RollbackTransaction());
//            }
//            public Mock<IMediator> MockMediator { get; set; }
//            public Mock<ITransactionUnitOfWork> MockUow { get; set; }
//        }
//        public class TestData
//        {
//            public TestData(string userName, string themeName)
//            {
//                User = new User { UserName = userName };
//                CreateUserThemeCommand = new CreateUserThemeCommand(
//                    User.UserName, 
//                    themeName, 
//                    "TestDescription", 
//                    true);
//                GetUserByNameQuery = new GetUserByNameQuery(CreateUserThemeCommand.UserName);
//                CreateThemeCommand = new CreateThemeCommand(
//                    CreateUserThemeCommand.ThemeName, 
//                    CreateUserThemeCommand.Description, 
//                    CreateUserThemeCommand.IsPublic);
//                LinkUserToTheme = new LinkUserToThemeCommand(
//                    User.UserName, 
//                    CreateThemeCommand.ThemeName, 
//                    TypeAccess.Read | TypeAccess.Write);
//            }
//            public User User { get; set; } 
//            public CreateUserThemeCommand CreateUserThemeCommand { get; set; } 
//            public GetUserByNameQuery GetUserByNameQuery { get; set; }
//            public CreateThemeCommand CreateThemeCommand { get; set; }
//            public LinkUserToThemeCommand LinkUserToTheme { get; set; }

//        }
//    }
//}
