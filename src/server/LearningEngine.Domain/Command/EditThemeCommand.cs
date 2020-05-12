using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class EditThemeCommand : IRequest, IPipelinePermission
    {
        public ThemeDto ThemeDto { get; private set; }
        public int UserId { get; private set; }
        public int ThemeId { get; private set; }
        public TypeAccess Access { get; private set; }

        public EditThemeCommand(ThemeDto themeDto, int userId, int themeId, TypeAccess access)
        {
            ThemeDto = themeDto;
            UserId = userId;
            ThemeId = themeId;
            Access = access;
        }
    }
}
