using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Query;
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
    public class GetThemeByNoteIdHandler : IRequestHandler<GetThemeByNoteIdQuery, ThemeDto>
    {
        private readonly LearnEngineContext context;

        public GetThemeByNoteIdHandler(LearnEngineContext context)
        {
            this.context = context;
        }

        public async Task<ThemeDto> Handle(GetThemeByNoteIdQuery request, CancellationToken cancellationToken)
        {
            var note = await context.Notes.FirstOrDefaultAsync(note => note.Id == request.NoteId);

            if (note == null)
            {
                throw new NoteNotFoundException();
            }

            var theme = await context.Themes.FirstOrDefaultAsync(theme => theme.Id == note.ThemeId);

            return new ThemeDto
            {
                Id = theme.Id,
                Desсription = theme.Description,
                Name = theme.Name,
                IsPublic = theme.IsPublic
            };
        }
    }
}
