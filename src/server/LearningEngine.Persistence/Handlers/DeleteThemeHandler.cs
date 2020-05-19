using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    public class DeleteThemeHandler : IRequestHandler<DeleteThemeCommand>
    {
        private readonly LearnEngineContext _context;
        public DeleteThemeHandler(LearnEngineContext context)
        {
            _context = context;
        }
        //deleting theme without deleting the subthemes
        public async Task<Unit> Handle(DeleteThemeCommand request, CancellationToken cancellationToken)
        {
            var theme = await _context.Themes.FirstOrDefaultAsync(theme => theme.Id == request.ThemeId);

            if(theme == null)
            {
                throw new Exception(ExceptionDescriptionConstants.ThemeNotFound);
            }

            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();

            return default;
        }
    }
}
