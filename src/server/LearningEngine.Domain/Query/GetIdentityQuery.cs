using MediatR;
using System.Security.Claims;

namespace LearningEngine.Domain.Query
{
    public class GetIdentityQuery : IRequest<ClaimsIdentity>
    {
        public GetIdentityQuery(string username, string password)
        {
            UserName = username;
            Password = password;
        }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
