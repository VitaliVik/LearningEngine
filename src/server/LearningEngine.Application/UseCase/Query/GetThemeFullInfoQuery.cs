using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Application.UseCase.Query
{
    public class GetThemeFullInfoQuery : IRequest<ThemeDto>, IPipelinePermissionQuery
    {
        public int UserId { get; private set; }
        public int ThemeId { get; private set; }

        public GetThemeFullInfoQuery(int userId, int themeId)
        {
            UserId = userId;
            ThemeId = themeId;
        }
    }
}
