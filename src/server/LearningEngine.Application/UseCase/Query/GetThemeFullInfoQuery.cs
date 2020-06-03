using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Application.UseCase.Query
{
    public class GetThemeFullInfoQuery : IRequest<ThemeDto>, IPipelinePermissionQuery
    {
        public int UserId { get; private set; }
        public int ObjectId { get; private set; }
        public ObjectType ObjectType { get; private set; }
        public GetThemeFullInfoQuery(int userId, int themeId)
        {
            UserId = userId;
            ObjectId = themeId;
            ObjectType = ObjectType.Theme;
        }
    }
}
