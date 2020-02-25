using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Persistence.Utils
{
    public interface IPasswordHasher
    {
        byte[] GetHash(string password, string username);
    }
}
