using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using LearningEngine.Application.Command;
using System.Threading.Tasks;
using System.Threading;
using LearningEngine.Persistence.Models;
using System.Linq;

namespace LearningEngine.Persistence.Handlers
{
    class CreateThemeHandler : IRequestHandler<CreateThemeCommand, bool>
    {
        private readonly LearnEngineContext _context;
        public CreateThemeHandler(LearnEngineContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(CreateThemeCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(usr => usr.UserName == request.UserName);
            if (user != null)
            {
                var theme = new Theme
                {
                    Name = request.ThemeName,
                    IsPublic = request.IsPublic,
                    Description = request.Description,
                    ParentThemeId = request.ParentThemeId
                };
                _context.Themes.Add(theme);
                await _context.SaveChangesAsync();
                var permission = new Permission
                {
                    UserId = user.Id,
                    ThemeId = theme.Id,
                    Access = TypeAccess.Read | TypeAccess.Write
                };
                _context.Permissions.Add(permission);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
