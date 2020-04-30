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

namespace LearningEngine.Persistence.Handlers
{
    public class GetThemeSubThemesHandler : IRequestHandler<GetThemeSubThemesQuery, List<ThemeHeaderDto>>
    {
        readonly LearnEngineContext _context;

        public GetThemeSubThemesHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public async Task<List<ThemeHeaderDto>> Handle(GetThemeSubThemesQuery request, CancellationToken cancellationToken)
        {
            var theme = await _context.Themes
                .Include(thm => thm.SubThemes)
                .FirstOrDefaultAsync(thm => thm.Id == request.ThemeId);

            if (theme != null)
            {
                var themes = theme.SubThemes
                    .Select(thm => new ThemeHeaderDto 
                    {
                        Id = thm.Id, 
                        Name = thm.Name, 
                        Description = thm.Description, 
                        IsPublic = thm.IsPublic 
                    })
                    .ToList();

                return themes;
            }
            else
            {
                throw new Exception("тема не найдена");
            }
        }
    }
}
