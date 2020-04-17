using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Query;
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
    class GetRootThemesByUserNameHandler : IRequestHandler<GetRootThemesByUserIdQuery, List<ThemeHeaderDto>>
    {
        private readonly LearnEngineContext _context;
        public GetRootThemesByUserNameHandler(LearnEngineContext _context)
        {
            this._context = _context;
        }
        public Task<List<ThemeHeaderDto>> Handle(GetRootThemesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var themes = _context.Permissions
                .Where(permission => permission.UserId == request.UserId && permission.Theme.ParentThemeId == null)
                .Select(permission => permission.Theme);
            if (themes.Count() == 0)
            {
                throw new Exception("User has no root themes connected with him");
            }

            var result = themes.Select(theme => new ThemeHeaderDto
            {
                Desription = theme.Description,
                IsPublic = theme.IsPublic,
                Name = theme.Name,
                Notes = theme.Notes.Select(note => new NoteDto
                {
                    Content = note.Content,
                    Title = note.Title
                }).ToList(),
            }).ToListAsync();

            return result;
        }
    }
}
