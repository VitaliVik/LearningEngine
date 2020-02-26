using LearningEngine.Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace LearningEngine.UnitTests.Utils
{
    
    public class PasswordHasherTests
    {

        [Theory]
        [InlineData("rolit", "123")]
        [InlineData("Kekekekekekekekekekek", "123456789")]
        public void ArrayMayHave64Symbols(string username, string password)
        {
            //arange
            PasswordHasher hasher = new PasswordHasher();

            //act
            byte[] result = hasher.GetHash(password, username);

            //assert
            Assert.NotNull(result);
            Assert.Equal(64, result.Length);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "rolit")]
        [InlineData("123", "")]
        [InlineData("123", null)]
        [InlineData(null, "rolit")]
        public void ThrowExceptionWhenIncorrectData(string username, string password)
        {
            //arange
            PasswordHasher hasher = new PasswordHasher();

            //act
            Action act = () => hasher.GetHash(password, username);

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValueMayBeEqual()
        {
            //arange
            PasswordHasher hasher = new PasswordHasher();

            //act
            byte[] result1 = hasher.GetHash("rolit", "123");
            byte[] result2 = hasher.GetHash("rolit", "123");

            //assert
            Assert.True(result1.SequenceEqual(result2));
        }
    }
}
