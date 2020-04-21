using LearningEngine.Domain.Command;
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
    public class LinkUserToThemeHandler : IRequestHandler<LinkUserToThemeCommand>
    {
        private readonly LearnEngineContext _context;
        public LinkUserToThemeHandler(LearnEngineContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(LinkUserToThemeCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(usr => usr.UserName == request.UserName);
            var theme = await _context.Themes
                .FirstOrDefaultAsync(thm => thm.Name == request.ThemeName);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            if (theme == null)
            {
                throw new Exception("Тема не найдена");
            }
            var permission = new Permission
            {
                ThemeId = theme.Id,
                UserId = user.Id,
                Access = request.Access
            };
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();

            return default;
        }
    }
}
