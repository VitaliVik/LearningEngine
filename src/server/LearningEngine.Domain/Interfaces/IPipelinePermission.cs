using LearningEngine.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Interfaces
{
    public interface IPipelinePermission
    {
        public int UserId { get; }
        public int ThemeId { get; }
        public TypeAccess Access { get; }
    }
}
