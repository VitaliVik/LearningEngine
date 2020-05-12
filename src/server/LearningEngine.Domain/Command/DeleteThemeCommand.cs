using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class DeleteThemeCommand : IRequest, IPipelinePermission
    {
        public DeleteThemeCommand(int themeId, int userId, TypeAccess access)
        {
            ThemeId = themeId;
            UserId = userId;
            Access = access;
        }
        public int ThemeId { get; private set; }
        public int UserId { get; private set; }
        public TypeAccess Access { get; private set; }
    }
}
