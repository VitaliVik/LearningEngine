using FluentValidation;
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

    public class GetIdentityQueryValidator : AbstractValidator<GetIdentityQuery>
    {
        public GetIdentityQueryValidator()
        {
            RuleFor(identity => identity.Password).NotNull().NotEmpty();
            RuleFor(identity => identity.UserName).NotNull().NotEmpty();
        }
    }
}
