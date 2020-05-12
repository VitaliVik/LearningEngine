using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.UseCase.Query
{
    public class GetThemeFullInfoQuery : IRequest<ThemeDto>, IPipelinePermission
    {
        public int UserId { get; private set; }
        public int ThemeId { get; private set; }
        public TypeAccess Access { get; private set; }

        public GetThemeFullInfoQuery(int userId, int themeId, TypeAccess access)
        {
            UserId = userId;
            ThemeId = themeId;
            Access = access;
        }
    }
}
