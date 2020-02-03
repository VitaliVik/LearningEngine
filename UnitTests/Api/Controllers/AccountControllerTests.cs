using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using LearningEngine.Api.Controllers;
using Moq;
using MediatR;
using LearningEngine.Application.Command;
using System.Threading;
using LearningEngine.Api.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace UnitTests.Api.Controllers
{
    public class AccountControllerTests
    {
        readonly Mock<Mediator> _mock;
        public AccountControllerTests()
        {
            _mock = new Mock<Mediator>();
            _mock.Setup(mediator => mediator.Send(It.IsAny<RegisterUserCommand>(), CancellationToken.None));
        }

    }
}
