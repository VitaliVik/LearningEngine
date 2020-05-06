using LearningEngine.Domain.DTO;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class EditThemeCommand : IRequest
    {
        public ThemeDto ThemeDto { get; private set; }
        public int UserId { get; private set; }
        public int ThemeId { get; private set; }
        public EditThemeCommand(ThemeDto themeDto, int userId, int themeId)
        {
            ThemeDto = themeDto;
            UserId = userId;
            ThemeId = themeId;
        }
    }
}
