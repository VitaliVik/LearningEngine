using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Enum
{
    [Flags]
    public enum TypeAccess
    {
        Read,
        Write
    }
}
