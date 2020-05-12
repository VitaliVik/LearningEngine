using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;

namespace LearningEngine.Domain.Query
{
    public class GetThemeNotesQuery : IRequest<List<NoteDto>>, IPipelinePermissionModel
    {
        public int ThemeId { get; private set; }

        public int UserId { get; private set; }

        public TypeAccess Access { get; private set; }

        public GetThemeNotesQuery(int themeId, int userId, TypeAccess access)
        {
            ThemeId = themeId;
            UserId = userId;
            Access = access;
        }
    }
}
