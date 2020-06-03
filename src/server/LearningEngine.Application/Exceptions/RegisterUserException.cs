using LearningEngine.Domain.Constants;
using System;

namespace LearningEngine.Application.Exceptions
{
    public class RegisterUserException : Exception
    {
        public RegisterUserException(Exception innerException)
            : base(ExceptionDescriptionConstants.RegistrationError, innerException)
        {
        }
    }
}
