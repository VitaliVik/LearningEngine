using LearningEngine.Application.Exceptions;
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
        private readonly LearnEngineContext context;

        public DeleteThemeHandler(LearnEngineContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteThemeCommand request, CancellationToken cancellationToken)
        {
            var theme = await context.Themes.FirstOrDefaultAsync(theme => theme.Id == request.ThemeId);

            if (theme == null)
            {
                throw new ThemeNotFoundException();
            }

            context.Themes.Remove(theme);
            await context.SaveChangesAsync();

            return default;
        }
    }
}
