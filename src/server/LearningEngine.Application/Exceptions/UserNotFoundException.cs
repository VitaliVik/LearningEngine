using LearningEngine.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class UserNotFoundException : BaseAppException
    {
        public UserNotFoundException()
            : base(ExceptionDescriptionConstants.UserNotFound)
        {
        }
    }
}
