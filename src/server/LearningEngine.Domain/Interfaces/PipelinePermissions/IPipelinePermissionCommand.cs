using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Interfaces.PipelinePermissions
{
    public interface IPipelinePermissionCommand
    {
        int UserId { get; }
        int ThemeId { get; }
    }
}
