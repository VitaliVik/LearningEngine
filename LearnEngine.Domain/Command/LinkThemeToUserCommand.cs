using MediatR;

namespace LearningEngine.Domain.Command
{
    public class LinkThemeToUserCommand : IRequest
    {
        public string UserName { get; private set; }
        public string ThemeName { get; private set; }
        public LinkThemeToUserCommand(string userName, string themeName)
        {
            UserName = userName;
            ThemeName = themeName;
        }
    }
}
