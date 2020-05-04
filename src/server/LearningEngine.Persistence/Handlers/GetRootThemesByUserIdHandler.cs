using LearningEngine.Domain.Constants;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
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
    public class GetRootThemesByUserIdHandler : IRequestHandler<GetRootThemesByUserIdQuery, List<ThemeHeaderDto>>
    {
        private readonly LearnEngineContext _context;
        public GetRootThemesByUserIdHandler(LearnEngineContext _context)
        {
            this._context = _context;
        }
        public Task<List<ThemeHeaderDto>> Handle(GetRootThemesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var themes = _context.Permissions
                .Where(permission => permission.UserId == request.UserId 
                && permission.Theme.ParentThemeId == null)
                .Select(permission => permission.Theme);
            if (!themes.Any())
            {
                throw new Exception(ExceptionDescriptionConstants.RootThemesNotFount);
            }

            var result = themes.Select(theme => new ThemeHeaderDto
            {
                Id = theme.Id,
                Name = theme.Name
            }).ToListAsync();

            return result;
        }
    }
}
