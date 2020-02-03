using System;

namespace LearningEngine.Application.Exceptions
{
    public class RegisterUserException : Exception
    {
        public RegisterUserException(Exception innerException)
            : base("Ошибка регистрации пользователя", innerException)
        {
        }
    }
}
