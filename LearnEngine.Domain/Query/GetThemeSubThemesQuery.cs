﻿using LearningEngine.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Query
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