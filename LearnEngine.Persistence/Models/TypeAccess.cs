using System;
using System.Collections.Generic;
using System.Text;

namespace LearnEngine.Persistence.Models
{
    [Flags]
    public enum TypeAccess
    {
        Read,
        Write
    }
}
