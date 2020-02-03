using LearningEngine.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;


namespace LearningEngine.Application.Query
{
    public class GetThemeCardsQuery: IRequest<List<CardDto>>
    {
        public string ThemeName { get; private set; }
        public GetThemeCardsQuery(string themeName)
        {
            ThemeName = themeName;
        }
    }
}
