using LearningEngine.Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using System.Security.Cryptography;

namespace LearningEngine.UnitTests.Utils
{
    public class PasswordHasherTests
    {
        [Theory]
        [InlineData("rolit", "123")]
        [InlineData("Kekekekekekekekekekek", "123456789")]
        public void ArrayMayHave64Symbols(string username, string password)
        {
            // arange
            PasswordHasher hasher = new PasswordHasher();

            // act
            byte[] result = hasher.GetHash(password, username);

            // assert
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
            // arange
            PasswordHasher hasher = new PasswordHasher();

            // act
            Action act = () => hasher.GetHash(password, username);

            // assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ValueMayBeEqual()
        {
            // arange
            PasswordHasher hasher = new PasswordHasher();
            byte[] expected = Convert.FromBase64String("Ihb7p9dl8NU3Yhuf+He/34s1gwXx4M9Ts85Msr9ehmacrN7SSm" +
                "sWag4bsYhOxdCI11r1apHbMglq//tCb/SioQ==");

            // act
            byte[] result1 = hasher.GetHash("123", "rolit");

            // assert
            Assert.Equal(expected, result1);
        }
    }
}
