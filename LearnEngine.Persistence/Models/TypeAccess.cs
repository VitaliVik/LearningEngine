using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Persistence.Models
{
    [Flags]
    public enum TypeAccess
    {
        Read,
        Write
    }
}
