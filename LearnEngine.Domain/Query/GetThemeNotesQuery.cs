using LearningEngine.Domain.DTO;
using MediatR;
using System.Collections.Generic;

namespace LearningEngine.Domain.Query
{
    public class GetThemeNotesQuery : IRequest<List<NoteDto>>
    {
        public int ThemeId { get; private set; }
        public GetThemeNotesQuery(int themeId)
        {
            ThemeId = themeId;
        }
    }
}
