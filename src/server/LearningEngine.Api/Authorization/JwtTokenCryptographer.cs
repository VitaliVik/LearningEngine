using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearningEngine.Api.Authorization
{
    public class JwtTokenCoder : IJwtTokenCryptographer
    {
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public JwtTokenCoder(JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        public string Encode(ClaimsIdentity claimsIdentity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
                                           audience: AuthOptions.AUDIENCE,
                                           claims: claimsIdentity.Claims,
                                           expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                                           signingCredentials: new Microsoft.IdentityModel.Tokens
                                           .SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), 
                                                               SecurityAlgorithms.HmacSha256Signature));
            return jwtSecurityTokenHandler.WriteToken(jwt);
        }
    }
}
