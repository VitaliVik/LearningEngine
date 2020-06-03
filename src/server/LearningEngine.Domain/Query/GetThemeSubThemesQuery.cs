using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;
using System.Collections.Generic;

namespace LearningEngine.Domain.Query
{
    public class GetThemeSubThemesQuery : IRequest<List<ThemeDto>>, IPipelinePermissionQuery
    {
        public int ObjectId { get; private set; }
        public int UserId { get; private set; }

        public ObjectType ObjectType => ObjectType.Theme;

        public GetThemeSubThemesQuery(int objectId, int userId)
        {
            ObjectId = objectId;
            UserId = userId;
        }
    }
}
