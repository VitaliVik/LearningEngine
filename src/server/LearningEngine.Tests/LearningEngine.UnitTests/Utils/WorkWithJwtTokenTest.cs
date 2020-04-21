using LearningEngine.Api.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace LearningEngine.UnitTests.Utils
{
    public class WorkWithJwtTokenTest
    {
        [Theory]
        [InlineData("vasyan", "23", "krytoi chel")]
        [InlineData("mashenko", "60", "syka prepod")]
        [InlineData("bodyanka", "20", "starsta")]
        public void WorkWithJwtToken_WithValidUserName_ShouldReturnUserClaims(string userName, string id, string role)
        {
            //arange
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim> { new Claim("UserName", userName),
            new Claim("UserId", id), new Claim("Role", role) });
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            JwtTokenCoder workWithJwtToken = new JwtTokenCoder(jwtSecurityTokenHandler);

            //act
            var jwtToken = workWithJwtToken.Encode(claimsIdentity);

            //assert
            var claims = jwtSecurityTokenHandler.ReadJwtToken(jwtToken).Claims.ToList();
            Assert.Equal(claims[0].Value, userName);
            Assert.Equal(claims[1].Value, id);
            Assert.Equal(claims[2].Value, role);
        }
    }
}
