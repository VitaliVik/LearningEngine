using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.IntegrationTests.Fixtures.Mocks
{
    public class HasherTestData
    {
        public HasherTestData()
        {
            Result = new byte[64];
        }
        public HasherTestData(byte[] array)
        {
            Result = array;
        }
        public byte[] Result { get; set; }
    }
}
