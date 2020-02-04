using LearningEngine.Domain.DTO;
using MediatR;

namespace LearningEngine.Domain.Query
{
    public class GetThemeHeaderQuery : IRequest<ThemeDto>
    {
        public GetThemeHeaderQuery(string themeName)
        {
            ThemeName = themeName;
        }

        public string ThemeName { get; private set; }
    }
}
