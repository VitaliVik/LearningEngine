using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Domain.Query
{
    public class GetThemeHeaderQuery : IRequest<ThemeDto>, IPipelinePermissionQuery
    {
        public GetThemeHeaderQuery(int objectId, int userId)
        {
            ObjectId = objectId;
            UserId = userId;
            ObjectType = ObjectType.Theme;
        }

        public int ObjectId { get; private set; }

        public int UserId { get; private set; }

        public ObjectType ObjectType { get; private set; }
    }
}
