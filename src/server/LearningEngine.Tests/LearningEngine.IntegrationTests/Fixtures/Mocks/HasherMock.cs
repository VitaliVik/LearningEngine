using LearningEngine.Persistence.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.IntegrationTests.Fixtures.Mocks
{
    public class HasherMocks
    {
        public Mock<IPasswordHasher> HasherMock { get; }
        public HasherMocks(HasherTestData td)
        {
            HasherMock = new Mock<IPasswordHasher>();
            HasherMock.Setup(m => m.GetHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(td.Result);
        }

    }
}
