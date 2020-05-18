using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Interfaces.PipelinePermissions
{
    public interface IPipelinePermissionCommand
    {
        public int UserId { get; }
        public int ThemeId { get; }
    }
}
