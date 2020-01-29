using LearningEngine.Persistence.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LearningEngine.Application.Query
{
    public class GetIdentityQuery: IRequest<ClaimsIdentity>
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
