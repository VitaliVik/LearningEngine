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
}
