using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Query;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    public class GetThemeNotesHandler : IRequestHandler<GetThemeNotesQuery, List<NoteDto>>
    {
        private readonly LearnEngineContext context;

        public GetThemeNotesHandler(LearnEngineContext context)
        {
            this.context = context;
        }

        public async Task<List<NoteDto>> Handle(GetThemeNotesQuery request, CancellationToken cancellationToken)
        {
            var theme = await context.Themes
                .Include(thm => thm.Notes)
                .FirstOrDefaultAsync(thm => thm.Id == request.ThemeId);

            if (theme != null)
            {
                var notes = theme.Notes
                    .Select(note => new NoteDto { Content = note.Content, Title = note.Title })
                    .ToList();
                return notes;
            }
            else
            {
                throw new ThemeNotFoundException();
            }
        }
    }
}
