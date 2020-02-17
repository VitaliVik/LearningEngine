using LearningEngine.Domain.Enum;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class LinkUserToThemeCommand : IRequest
    {
        public string UserName { get; private set; }
        public string ThemeName { get; private set; }
        public TypeAccess Access { get; private set; }
        public LinkUserToThemeCommand(string userName, string themeName, TypeAccess access)
        {
            UserName = userName;
            ThemeName = themeName;
            Access = access;
        }
    }
}
