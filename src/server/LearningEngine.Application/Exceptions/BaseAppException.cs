using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class BaseAppException : Exception
    {
        public BaseAppException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public BaseAppException(string message) : base(message)
        {

        }
    }
}
