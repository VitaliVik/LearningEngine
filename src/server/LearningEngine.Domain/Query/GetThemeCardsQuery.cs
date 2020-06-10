using FluentValidation;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;
using System.Collections.Generic;

namespace LearningEngine.Domain.Query
{
    public class GetThemeCardsQuery : IRequest<List<CardDto>>, IPipelinePermissionQuery
    {
        public int ThemeId { get; private set; }

        public int UserId { get; private set; }

        public ObjectType ObjectType => ObjectType.Theme;

        public int ObjectId => ThemeId;

        public GetThemeCardsQuery(int themeId, int userId)
        {
            ThemeId = themeId;
            UserId = userId;
        }
    }

    public class GetThemeCardsQueryValidator : AbstractValidator<GetThemeCardsQuery>
    {
        public GetThemeCardsQueryValidator()
        {
            RuleFor(obj => obj.ThemeId).GreaterThan(0);
            RuleFor(obj => obj.UserId).GreaterThan(0);
        }
    }
}
