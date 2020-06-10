using LearningEngine.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class SimpleValidationException : BaseAppException
    {
        public SimpleValidationException(string message) 
            : base(message)
        {
        }
    }
}
