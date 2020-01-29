using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningEngine.Api.Authorization
{
    public class AuthOptions
    {
        public const string ISSUER = "LearningEngine.Api.Authorization";
        public const string AUDIENCE = "LearningEngine.Client";
        public const string KEY = "1234";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
