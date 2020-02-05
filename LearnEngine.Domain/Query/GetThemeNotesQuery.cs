using LearningEngine.Domain.DTO;
using MediatR;
using System.Collections.Generic;

namespace LearningEngine.Domain.Query
{
    public class GetThemeNotesQuery : IRequest<List<NoteDto>>
    {
        public string ThemeNameId { get; private set; }
        public GetThemeNotesQuery(string themeName)
        {
            ThemeNameId = themeName;
        }
    }
}
