using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using LearningEngine.Persistence.Models;
using System.Diagnostics.CodeAnalysis;

namespace LearningEngine.Persistence.Utils
{
    public class PasswordHasher: IPasswordHasher
    {
        public byte[] GetHash([NotNull]string password, [NotNull]string salt)
        {
            var hasher = SHA512.Create();
            var byteArray = hasher.ComputeHash(Encoding.ASCII.GetBytes(password + salt));
            return byteArray;
        }
    }
}
