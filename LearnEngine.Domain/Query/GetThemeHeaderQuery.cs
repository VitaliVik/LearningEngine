using LearningEngine.Domain.DTO;
using MediatR;

namespace LearningEngine.Domain.Query
{
    public class GetThemeHeaderQuery : IRequest<ThemeDto>
    {
        public GetThemeHeaderQuery(int themeId)
        {
            ThemeId = themeId;
        }

        public int ThemeId { get; private set; }
    }
}
