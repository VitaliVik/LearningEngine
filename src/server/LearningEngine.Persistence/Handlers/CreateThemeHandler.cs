using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Enum;
using LearningEngine.Persistence.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    public class CreateThemeHandler : IRequestHandler<CreateThemeCommand, int>
    {
        private readonly LearnEngineContext _context;
        public CreateThemeHandler(LearnEngineContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateThemeCommand request, CancellationToken cancellationToken)
        {
            try
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
                return theme.Id;
            }
            catch (Exception ex)
            {
                throw new CreateThemeException(ex);
            }

        }


    }
}
