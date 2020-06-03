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
    public  class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly LearnEngineContext _context;
        private readonly IPasswordHasher _hasher;
        public RegisterUserHandler(LearnEngineContext context, IPasswordHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }


        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
            };
            user.Password = _hasher.GetHash(request.Password, request.UserName);
            await _context.Users.AddAsync(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RegisterUserException(ex);
            }

            return default;
        }


    }
}
