using LearningEngine.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class CardNotFoundException : BaseAppException
    {
        public CardNotFoundException()
            : base(ExceptionDescriptionConstants.CardNotFound)
        {
        }
    }
}
