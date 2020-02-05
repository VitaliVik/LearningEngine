using LearningEngine.Domain.DTO;
using MediatR;

namespace LearningEngine.Domain.Query
{
    public class GetThemeHeaderQuery : IRequest<ThemeDto>
    {
        public GetThemeHeaderQuery(string themeName)
        {
            ThemeNameId = themeName;
        }

        public string ThemeNameId { get; private set; }
    }
}
