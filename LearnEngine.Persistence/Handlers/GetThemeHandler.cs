using LearningEngine.Application.DTO;
using LearningEngine.Application.Exceptions;
using LearningEngine.Application.Query;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    class GetThemeHandler : IRequestHandler<GetThemeQuery, ThemeDto>
    {
        LearnEngineContext _context;
        public GetThemeHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public Task<ThemeDto> Handle(GetThemeQuery request, CancellationToken cancellationToken)
        {
            var theme = _context.Themes
                .Include(thm => thm.SubThemes)
                .Include(thm => thm.Notes)
                .Include(thm => thm.ParentTheme)
                .FirstOrDefault(thm => thm.Name == request.ThemeName);
            try
            {
                if (theme != null)
                {
                    var themeDto = new ThemeDto
                    {
                        Name = theme.Name,
                        Desription = theme.Description,
                        IsPublic = theme.IsPublic,
                        Notes = theme.Notes
                            .Select(note => new NoteDto { Title = note.Title, Content = note.Content })
                            .ToList(),
                        ParentTheme = (theme.ParentTheme != null ? new ThemeDto
                        {
                            Name = theme.ParentTheme.Name,
                            Desription = theme.ParentTheme.Description,
                            IsPublic = theme.ParentTheme.IsPublic
                        }
                        : null)
                    };
                    return Task.Run(() => themeDto);
                    
                }
                else
                {
                    throw new Exception("тема не найдена");
                }
            }
            catch (Exception ex)
            {

                throw new GetThemeHandlerException(ex);
            }
        }
    }
}
