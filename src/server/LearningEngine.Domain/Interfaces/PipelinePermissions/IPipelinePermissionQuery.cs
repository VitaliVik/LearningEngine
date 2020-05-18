using LearningEngine.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Interfaces.PipelinePermissions
{
    public interface IPipelinePermissionQuery
    {
        public int UserId { get; }
        public int ThemeId { get; }
    }
}
