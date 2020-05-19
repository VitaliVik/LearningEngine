using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;
using System.Collections.Generic;

namespace LearningEngine.Domain.Query
{
    public class GetThemeNotesQuery : IRequest<List<NoteDto>>, IPipelinePermissionQuery
    {
        public int ThemeId { get; private set; }

        public int UserId { get; private set; }


        public GetThemeNotesQuery(int themeId, int userId)
        {
            ThemeId = themeId;
            UserId = userId;
        }
    }
}
