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
        public byte[] GetHash(string password, string username)
        {
            if (password == "" || username == "" ||
                password == null || username == null)
            {
                throw new Exception("Неверный формат пароля или пользовательского имени");
            }
            var hasher = SHA512.Create();
            var byteArray = hasher.ComputeHash(Encoding.ASCII.GetBytes(password + username));
            return byteArray;
        }
    }
}
