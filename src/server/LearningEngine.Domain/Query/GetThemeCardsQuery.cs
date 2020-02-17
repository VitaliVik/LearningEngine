using LearningEngine.Domain.DTO;
using MediatR;
using System.Collections.Generic;


namespace LearningEngine.Domain.Query
{
    public class GetThemeCardsQuery : IRequest<List<CardDto>>
    {
        public int ThemeId { get; private set; }
        public GetThemeCardsQuery(int themeId)
        {
            ThemeId = themeId;
        }
    }
}
