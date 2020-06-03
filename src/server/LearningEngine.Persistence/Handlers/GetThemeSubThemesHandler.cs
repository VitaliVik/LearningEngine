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
using System.Linq;
using LearningEngine.Domain.Constants;
using LearningEngine.Application.Exceptions;

namespace LearningEngine.Persistence.Handlers
{
    public class GetThemeSubThemesHandler : IRequestHandler<GetThemeSubThemesQuery, List<ThemeDto>>
    {
        readonly LearnEngineContext _context;

        public GetThemeSubThemesHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public async Task<List<ThemeDto>> Handle(GetThemeSubThemesQuery request, CancellationToken cancellationToken)
        {
            var theme = await _context.Themes
                .Include(thm => thm.SubThemes)
                .FirstOrDefaultAsync(thm => thm.Id == request.ThemeId);

            if (theme != null)
            {
                var themes = theme.SubThemes
                    .Select(thm => new ThemeDto
                    {
                        Id = thm.Id, 
                        Name = thm.Name,
                        Desсription = thm.Description, 
                        IsPublic = thm.IsPublic,
                        Notes = thm.Notes?.Select(note => new NoteDto { Content = note.Content, Title = note.Title }).ToList()
                    })
                    .ToList();

                return themes;
            }
            else
            {
                throw new ThemeNotFoundException();
            }
        }
    }
}
