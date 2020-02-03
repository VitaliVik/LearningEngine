using LearningEngine.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Query
{
    public class GetThemeSubThemesQuery : IRequest<List<ThemeDto>>
    {
        public string ThemeName { get; private set; }
        public GetThemeSubThemesQuery(string themeName)
        {
            ThemeName = themeName;
        }
    }
}
