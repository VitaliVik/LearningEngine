using LearningEngine.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Query
{
    public class GetThemeNotesQuery: IRequest<List<NoteDto>>
    {
        public string ThemeName { get; private set; }
        public GetThemeNotesQuery(string themeName)
        {
            ThemeName = themeName;
        }
    }
}
