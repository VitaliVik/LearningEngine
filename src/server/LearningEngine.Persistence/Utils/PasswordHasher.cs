using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using LearningEngine.Persistence.Models;
using System.Diagnostics.CodeAnalysis;

namespace LearningEngine.Persistence.Utils
{
    static public class PasswordHasher
    {
        public static string GetHash([NotNull]string password, [NotNull]string salt)
        {
            var hasher = SHA512.Create();
            var byteArray = hasher.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            return Encoding.UTF8.GetString(byteArray);
        }
    }
}
