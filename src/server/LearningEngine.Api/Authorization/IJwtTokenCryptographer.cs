using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearningEngine.Api.Authorization
{
    public interface IJwtTokenCryptographer
    {
        string Encode(ClaimsIdentity claimsIdentity);
    }
}
