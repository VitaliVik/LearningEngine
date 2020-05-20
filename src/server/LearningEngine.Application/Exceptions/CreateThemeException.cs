using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class CreateThemeException : Exception
    {
        public CreateThemeException(Exception innerException): base("Ошибка при создании темы", innerException)
        {
        }
    }
}
