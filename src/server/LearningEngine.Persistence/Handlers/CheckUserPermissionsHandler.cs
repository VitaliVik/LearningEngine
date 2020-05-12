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
    public class CheckUserPermissionsHandler : IRequestHandler<CheckUserPermissionsQuery, bool>
    {
        private readonly LearnEngineContext _context;

        public CheckUserPermissionsHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CheckUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _context.Permissions.FirstOrDefaultAsync(permissions => permissions.ThemeId == request.ThemeId &&
                                                                                      permissions.UserId == request.UserId);
            
            if(permissions == null || permissions.Access == request.Access)
            {
                return false;
            }

            return true;
        }
    }
}
