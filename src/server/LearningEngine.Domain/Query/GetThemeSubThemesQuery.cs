using FluentValidation;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;
using System.Collections.Generic;

namespace LearningEngine.Domain.Query
{
    public class GetThemeSubThemesQuery : IRequest<List<ThemeDto>>, IPipelinePermissionQuery
    {
        public int ThemeId { get; private set; }

        public int UserId { get; private set; }

        public int ObjectId => ThemeId;

        public ObjectType ObjectType => ObjectType.Theme;

        public GetThemeSubThemesQuery(int themeId, int userId)
        {
            ThemeId = themeId;
            UserId = userId;
        }
    }

    public class GetThemeSubThemesQueryValidator : AbstractValidator<GetThemeSubThemesQuery>
    {
        public GetThemeSubThemesQueryValidator()
        {
            RuleFor(theme => theme.ThemeId).GreaterThan(0);
            RuleFor(theme => theme.UserId).GreaterThan(0);
        }
    }
}
