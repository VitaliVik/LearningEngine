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
        private readonly LearnEngineContext _context;

        public EditThemeHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(EditThemeCommand request, CancellationToken cancellationToken)
        {
            var theme = await _context.Themes.FirstOrDefaultAsync(theme => theme.Id == request.ThemeDto.Id);

            if (theme == null)
            {
                throw new Exception(ExceptionDescriptionConstants.ThemeNotFound);
            }

            var permission = await _context.Permissions.FirstOrDefaultAsync(permission => permission.UserId == request.UserId &&
                                                                                          permission.ThemeId == request.ThemeDto.Id);

            if(permission == null || permission.Access != TypeAccess.Write)
            {
                throw new Exception(ExceptionDescriptionConstants.NoRightsForEditing);
            }

            theme.Name = request.ThemeDto.Name;
            theme.Description = request.ThemeDto.Desсription;
            theme.IsPublic = request.ThemeDto.IsPublic;
            if(request.ThemeDto.Notes != null)
            {
                theme.Notes = request.ThemeDto.Notes.Select(note => new Note { Content = note.Content, Title = note.Title }).ToList();
            }

            _context.Themes.Update(theme);
            await _context.SaveChangesAsync();

            return default;
        }
    }
}
