using LearningEngine.Application.Exceptions;
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
        private readonly LearnEngineContext context;

        public GetRootThemesByUserIdHandler(LearnEngineContext context)
        {
            this.context = context;
        }

        public Task<List<ThemeHeaderDto>> Handle(GetRootThemesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var themes = context.Permissions
                .Where(permission => permission.UserId == request.UserId
                && permission.Theme.ParentThemeId == null)
                .Select(permission => permission.Theme);

            if (!themes.Any())
            {
                throw new RootThemesNotFoundException();
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
