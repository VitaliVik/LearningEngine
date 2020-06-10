using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Query;
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
    public class CheckUserThemePermissionsHandler : IRequestHandler<CheckUserThemePermissionsQuery>
    {
        private readonly LearnEngineContext context;

        public CheckUserThemePermissionsHandler(LearnEngineContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(CheckUserThemePermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await context.Permissions.FirstOrDefaultAsync(permissions => permissions.ThemeId == request.ThemeId &&
                                                                                      permissions.UserId == request.UserId);

            if (permissions == null || permissions.Access < request.Access)
            {
                throw new NoPermissionException();
            }

            return default;
        }
    }
}
