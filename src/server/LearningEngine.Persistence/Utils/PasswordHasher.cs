using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using LearningEngine.Persistence.Models;
using System.Diagnostics.CodeAnalysis;

namespace LearningEngine.Persistence.Utils
{
    public class PasswordHasher : IPasswordHasher
    {
        public byte[] GetHash(string password, string username)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Поле пароль имеет некорректное значение");
            }
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("Поле имя пользователя имеет некорректное значение");
            }
            var hasher = SHA512.Create();
            var byteArray = hasher.ComputeHash(Encoding.ASCII.GetBytes(password + username));

            return byteArray;
        }
    }
}
