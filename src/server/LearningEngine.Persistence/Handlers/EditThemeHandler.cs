using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    public class EditThemeHandler : IRequestHandler<EditThemeCommand>
    {
        private readonly LearnEngineContext context;

        public EditThemeHandler(LearnEngineContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(EditThemeCommand request, CancellationToken cancellationToken)
        {
            var theme = await context.Themes.FirstOrDefaultAsync(theme => theme.Id == request.ThemeDto.Id);

            if (theme == null)
            {
                throw new ThemeNotFoundException();
            }

            theme.Name = request.ThemeDto.Name;
            theme.Description = request.ThemeDto.Desсription;
            theme.IsPublic = request.ThemeDto.IsPublic;
            if (request.ThemeDto.Notes != null)
            {
                theme.Notes = request.ThemeDto.Notes.Select(note => new Note { Content = note.Content, Title = note.Title }).ToList();
            }

            context.Themes.Update(theme);
            await context.SaveChangesAsync();

            return default;
        }
    }
}
