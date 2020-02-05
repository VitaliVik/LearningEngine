using LearningEngine.Domain.DTO;
using MediatR;
using System.Collections.Generic;


namespace LearningEngine.Domain.Query
{
    public class GetThemeCardsQuery : IRequest<List<CardDto>>
    {
        public string ThemeNameId { get; private set; }
        public GetThemeCardsQuery(string themeName)
        {
            ThemeNameId = themeName;
        }
    }
}
