using LearningEngine.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Query
{
    public class GetThemeQuery: IRequest<ThemeDto>
    {
        public GetThemeQuery(string themeName)
        {
            ThemeName = themeName;
        }

        public string ThemeName { get; private set; }
    }
}
