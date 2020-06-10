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

        public byte[] Hash { get; }

        public HasherMocks()
        {
            Hash = new byte[64];
            HasherMock = new Mock<IPasswordHasher>();
            HasherMock.Setup(m => m.GetHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Hash);
        }

        public HasherMocks(byte[] byteArray)
        {
            HasherMock = new Mock<IPasswordHasher>();
            HasherMock.Setup(m => m.GetHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(byteArray);
            Hash = byteArray;
        }
    }
}
