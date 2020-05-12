using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Query
{
    public class GetThemeSubThemesQuery : IRequest<List<ThemeDto>>, IPipelinePermission
    {
        public int ThemeId { get; private set; }
        public int UserId { get; private set; }
        public TypeAccess Access { get; private set; }

        public GetThemeSubThemesQuery(int themeId, int userId, TypeAccess access)
        {
            ThemeId = themeId;
            UserId = userId;
            Access = access;
        }
    }
}
