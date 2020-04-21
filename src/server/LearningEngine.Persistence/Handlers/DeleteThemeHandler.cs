using LearningEngine.Domain.Command;
using LearningEngine.Domain.Enum;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    public class DeleteThemeHandler : IRequestHandler<DeleteThemeCommand>
    {
        private readonly LearnEngineContext _context;
        public DeleteThemeHandler(LearnEngineContext context)
        {
            _context = context;
        }
        //deleting theme without deleting the subthemes
        public async Task<Unit> Handle(DeleteThemeCommand request, CancellationToken cancellationToken)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(permission => permission.ThemeId == request.ThemeId 
                                                                            && permission.UserId == request.UserId);

            if(permission == null)
            {
                throw new Exception("Theme, connected to this user not found");
            }

            if(permission.Access != TypeAccess.Write)
            {
                throw new Exception("User has no rights to delete this theme");
            }

            var theme = await _context.Themes.FirstOrDefaultAsync(theme => theme.Id == permission.ThemeId);
            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();

            return default;
        }
    }
}
