using LearningEngine.Application.Exceptions;
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
        private readonly LearnEngineContext context;

        public LinkUserToThemeHandler(LearnEngineContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(LinkUserToThemeCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(usr => usr.Id == request.UserId);
            var theme = await context.Themes
                .FirstOrDefaultAsync(thm => thm.Id == request.ThemeId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (theme == null)
            {
                throw new ThemeNotFoundException();
            }

            var permission = new Permission
            {
                ThemeId = theme.Id,
                UserId = user.Id,
                Access = request.Access
            };
            await context.Permissions.AddAsync(permission);
            await context.SaveChangesAsync();

            return default;
        }
    }
}
