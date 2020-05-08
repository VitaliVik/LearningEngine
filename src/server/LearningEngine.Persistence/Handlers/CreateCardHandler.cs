using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    public class CreateCardHandler : IRequestHandler<CreateCardCommand>
    {
        private readonly LearnEngineContext _context;

        public CreateCardHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var theme = await _context.Themes.FirstOrDefaultAsync(theme => theme.Id == request.ThemeId);

            if (theme == null)
            {
                throw new Exception(ExceptionDescriptionConstants.ThemeNotFound);
            }

            var permissions = await _context.Permissions.FirstOrDefaultAsync(permission => permission.UserId == request.UserId &&
                                                                                           permission.ThemeId == request.ThemeId);
            if(permissions == null || permissions.Access != TypeAccess.Write)
            {
                throw new Exception(ExceptionDescriptionConstants.NoPermissions);
            }

            await _context.Cards.AddAsync(new Card { Answer = request.Answer, Question = request.Question, ThemeId = request.ThemeId });
            await _context.SaveChangesAsync();

            return default;
        }
    }
}
