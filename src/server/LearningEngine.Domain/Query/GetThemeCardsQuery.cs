using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;
using System.Collections.Generic;


namespace LearningEngine.Domain.Query
{
    public class GetThemeCardsQuery : IRequest<List<CardDto>>, IPipelinePermissionQuery
    {
        public int ThemeId { get; private set; }

        public int UserId { get; private set; }


        public GetThemeCardsQuery(int themeId, int userId)
        {
            ThemeId = themeId;
            UserId = userId;
        }
    }
}
