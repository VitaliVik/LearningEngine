using LearningEngine.Application.Command;
using LearningEngine.Persistence.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace LearningEngine.Persistence.Handlers
{
    public  class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly LearnEngineContext _context;
        public RegisterUserHandler(LearnEngineContext context)
        {
            _context = context;
        }


        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                Password = request.Password,
                UserName = request.UserName
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return default;
        }


    }
}
