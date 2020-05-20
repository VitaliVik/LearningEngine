using LearningEngine.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class NoPermissionException : BaseAppException
    {
        public NoPermissionException(Exception innerException)
            : base(ExceptionDescriptionConstants.NoPermissions, innerException)
        {

        }

        public NoPermissionException()
            : base(ExceptionDescriptionConstants.NoPermissions)
        {

        }
    }
}
