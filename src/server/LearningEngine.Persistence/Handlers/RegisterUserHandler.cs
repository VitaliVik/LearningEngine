using LearningEngine.Domain.Command;
using LearningEngine.Persistence.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using LearningEngine.Application.Exceptions;
using System;
using LearningEngine.Persistence.Utils;

namespace LearningEngine.Persistence.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly LearnEngineContext context;
        private readonly IPasswordHasher hasher;

        public RegisterUserHandler(LearnEngineContext context, IPasswordHasher hasher)
        {
            this.context = context;
            this.hasher = hasher;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
            };
            user.Password = hasher.GetHash(request.Password, request.UserName);
            await context.Users.AddAsync(user);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RegisterUserException(ex);
            }

            return default;
        }
    }
}
