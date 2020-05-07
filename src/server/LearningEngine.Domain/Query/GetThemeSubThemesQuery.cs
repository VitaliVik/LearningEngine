using LearningEngine.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Query
{
    public class GetThemeSubThemesQuery : IRequest<List<ThemeHeaderDto>>
    {
        public int ThemeId { get; private set; }
        public GetThemeSubThemesQuery(int themeId)
        {
            ThemeId = themeId;
        }
    }
}
