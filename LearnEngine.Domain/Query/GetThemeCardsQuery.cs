using LearningEngine.Domain.DTO;
using MediatR;
using System.Collections.Generic;


namespace LearningEngine.Domain.Query
{
    public class GetThemeCardsQuery : IRequest<List<CardDto>>
    {
        public string ThemeName { get; private set; }
        public GetThemeCardsQuery(string themeName)
        {
            ThemeName = themeName;
        }
    }
}
