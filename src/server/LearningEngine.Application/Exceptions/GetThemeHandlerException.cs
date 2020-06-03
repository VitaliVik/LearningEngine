using LearningEngine.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class GetThemeHandlerException : Exception
    {
        public GetThemeHandlerException(Exception innerException)
            : base(ExceptionDescriptionConstants.GettingThemeError, innerException)
        {
        }
    }
}
