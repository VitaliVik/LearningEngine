using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using LearningEngine.Domain.Query;
using LearningEngine.Persistence.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using System.Threading;
using LearningEngine.Persistence.Utils;

namespace LearningEngine.Persistence.Handlers
{
    public class GetIdentityHandler : IRequestHandler<GetIdentityQuery, ClaimsIdentity>
    {
        private readonly LearnEngineContext _context;
        public GetIdentityHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public Task<ClaimsIdentity> Handle(GetIdentityQuery request, CancellationToken cancellationToken)
        {
            var user = _context.Users
                .FirstOrDefault(usr => usr.UserName == request.UserName && usr.Password == PasswordHasher.GetHash(request.Password, request.UserName));
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return Task.Run(() => claimsIdentity);
            }
            return Task.Run(() => (ClaimsIdentity)null);
        }

    }
}
