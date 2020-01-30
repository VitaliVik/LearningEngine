using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using LearningEngine.Application.Query;
using LearningEngine.Persistence.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace LearningEngine.Persistence.Handlers
{
    public class GetIdentityHandler : RequestHandler<GetIdentityQuery, ClaimsIdentity>
    {
        private readonly LearnEngineContext _context;
        public GetIdentityHandler(LearnEngineContext context)
        {
            _context = context;
        }

        protected override ClaimsIdentity Handle(GetIdentityQuery request)
        {
            var user = _context.Users
                .FirstOrDefault(usr => usr.UserName == request.UserName && usr.Password == request.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}
