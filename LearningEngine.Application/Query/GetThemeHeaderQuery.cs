using LearningEngine.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Query
{
    public class GetThemeHeaderQuery: IRequest<ThemeDto>
    {
        public GetThemeHeaderQuery(string themeName)
        {
            ThemeName = themeName;
        }

        public string ThemeName { get; private set; }
    }
}
