using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Query;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    class GetThemeHeaderHandler : IRequestHandler<GetThemeHeaderQuery, ThemeDto>
    {
        readonly LearnEngineContext _context;
        public GetThemeHeaderHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public async Task<ThemeDto> Handle(GetThemeHeaderQuery request, CancellationToken cancellationToken)
        {
            var theme = await _context.Themes
                .Include(thm => thm.ParentTheme)
                .FirstOrDefaultAsync(thm => thm.Id == request.ThemeId);
            if (theme != null)
            {
                var themeDto = new ThemeDto { Id = theme.Id, Name = theme.Name, Desription = theme.Description, IsPublic = theme.IsPublic };
                if (theme.ParentTheme != null)
                {
                    themeDto.ParentTheme = new ThemeDto
                    {
                        Id = theme.ParentTheme.Id,
                        Name = theme.ParentTheme.Name,
                        Desription = theme.ParentTheme.Description,
                        IsPublic = theme.ParentTheme.IsPublic
                    };
                }
                return themeDto;
            }
            else
            {
                throw new Exception("тема не найдена");
            }

        }
    }
}
