using LearningEngine.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class ThemeNotFoundException : BaseAppException
    {
        public ThemeNotFoundException()
            : base(ExceptionDescriptionConstants.ThemeNotFound)
        {

        }
    }
}
