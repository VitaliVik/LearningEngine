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
        private readonly IPasswordHasher _hasher;
        public GetIdentityHandler(LearnEngineContext context, IPasswordHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public Task<ClaimsIdentity> Handle(GetIdentityQuery request, CancellationToken cancellationToken)
        {
            var user = _context.Users
                .FirstOrDefault(usr => usr.UserName == request.UserName);
            if (user != null)
            {
                var passwordCorrect = user.Password.SequenceEqual(_hasher.GetHash(request.Password, request.UserName));
                if (!passwordCorrect)
                {
                    return Task.FromResult((ClaimsIdentity)null);
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return Task.Run(() => claimsIdentity);
            }
            return Task.FromResult((ClaimsIdentity)null);
        }

    }
}
