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
        public HasherMocks()
        {
            HasherMock = new Mock<IPasswordHasher>();
            HasherMock.Setup(m => m.GetHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new byte[64]);
        }
        public HasherMocks(byte[] byteArray)
        {
            HasherMock = new Mock<IPasswordHasher>();
            HasherMock.Setup(m => m.GetHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(byteArray);
        }

    }
}
