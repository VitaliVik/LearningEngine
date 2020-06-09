using LearningEngine.Domain.Enum;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class LinkUserToThemeCommand : IRequest
    {
        public int UserId { get; private set; }

        public int ThemeId { get; private set; }

        public TypeAccess Access { get; private set; }

        public LinkUserToThemeCommand(int userId, int themeId, TypeAccess access)
        {
            UserId = userId;
            ThemeId = themeId;
            Access = access;
        }
    }
}
