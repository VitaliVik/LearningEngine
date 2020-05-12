using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;

namespace LearningEngine.Domain.Query
{
    public class GetThemeHeaderQuery : IRequest<ThemeDto>, IPipelinePermission
    {
        public GetThemeHeaderQuery(int themeId, int userId, TypeAccess access)
        {
            ThemeId = themeId;
            UserId = userId;
            Access = access;
        }

        public int ThemeId { get; private set; }

        public int UserId { get; private set; }

        public TypeAccess Access { get; private set; }
    }
}
